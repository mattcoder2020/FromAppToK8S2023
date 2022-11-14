using Common.Messages;
using Common.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using RawRabbit.Common;
using System.Threading.Tasks;
using System.Reflection;
using Polly;
using RawRabbit.Enrichers.MessageContext;
using Common.Exceptions;
using OpenTracing;

namespace Common.RabbitMQ
{
    public class BusSubscriber : BaseSubscriber, IBusSubscriber
    {
       
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly string _defaultNamespace;
      

        public BusSubscriber(IServiceProvider provider) : base(provider)
        {
            _logger = provider.GetService<ILogger<BusSubscriber>>();
            _serviceProvider = provider.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
            var options = _serviceProvider.GetService<RabbitMqOptions>();
            _defaultNamespace = options.Namespace;
            _retries = options.Retries >= 0 ? options.Retries : 3;
            _retryInterval = options.RetryInterval > 0 ? options.RetryInterval : 2;
           
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, Exception, IRejectedEvent> onError = null)
            where TCommand : ICommand
        {
             _busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, correlationContext) =>
            {
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                //return await TryHandleAsync(command, correlationContext,
                //    () => commandHandler.HandleAsync(command, correlationContext), onError);

                await BaseTryHandleAsync(command, correlationContext,
                    () => commandHandler.HandleAsync(command, correlationContext), onError);
                return new Ack();
            },
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TCommand>(@namespace, queueName)))));

            return this;
        }

        public async Task<IBusSubscriber> SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, Exception, IRejectedEvent> onError = null)
            where TEvent : IEvent
        {
            await _busClient.SubscribeAsync<TEvent, CorrelationContext>(async (@event, correlationContext) =>
            {
                var eventHandler = _serviceProvider.GetService<IEventHandler<TEvent>>();
                 
                //return await TryHandleAsync(@event, correlationContext,
                await BaseTryHandleAsync(@event, correlationContext,
                    () => eventHandler.HandleAsync(@event, correlationContext), onError);
                return new Ack(); 
            },
                ctx => ctx.UseSubscribeConfiguration(cfg =>
                    cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>(@namespace, queueName)))));

            return this;
        }

       

        // RabbitMQ retry that will publish a message to the retry queue.
        // Keep in mind that it might get processed by the other services using the same routing key and wildcards.
        private async Task<Acknowledgement> TryHandleWithRequeuingAsync<TMessage>(TMessage message,
            CorrelationContext correlationContext,
            Func<Task> handle, Func<TMessage, Exception, IRejectedEvent> onError = null)
        {
            var messageName = message.GetType().Name;
            var retryMessage = correlationContext.Retries == 0
                ? string.Empty
                : $"Retry: {correlationContext.Retries}'.";
            _logger.LogInformation($"Handling a message: '{messageName}' " +
                                   $"with correlation id: '{correlationContext.Id}'. {retryMessage}");

            try
            {
                await handle();
                _logger.LogInformation($"Handled a message: '{messageName}' " +
                                       $"with correlation id: '{correlationContext.Id}'. {retryMessage}");

                return new Ack();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                if (exception is Exception dShopException && onError != null)
                {
                    var rejectedEvent = onError(message, dShopException);
                    await _busClient.PublishAsync(rejectedEvent, ctx => ctx.UseMessageContext(correlationContext));
                    _logger.LogInformation($"Published a rejected event: '{rejectedEvent.GetType().Name}' " +
                                           $"for the message: '{messageName}' with correlation id: '{correlationContext.Id}'.");

                    return new Ack();
                }

                if (correlationContext.Retries >= _retries)
                {
                    await _busClient.PublishAsync(RejectedEvent.For(messageName),
                        ctx => ctx.UseMessageContext(correlationContext));

                    throw new Exception($"Unable to handle a message: '{messageName}' " +
                                        $"with correlation id: '{correlationContext.Id}' " +
                                        $"after {correlationContext.Retries} retries.", exception);
                }

                _logger.LogInformation($"Unable to handle a message: '{messageName}' " +
                                       $"with correlation id: '{correlationContext.Id}', " +
                                       $"retry {correlationContext.Retries}/{_retries}...");

                return Retry.In(TimeSpan.FromSeconds(_retryInterval));
            }
        }

        //(@namespace: "Matt-Product", queueName : "ProductCreated",
        private string GetQueueName<T>(string @namespace = null, string name = null)
        {
            @namespace = string.IsNullOrWhiteSpace(@namespace)
                ? (string.IsNullOrWhiteSpace(_defaultNamespace) ? string.Empty : _defaultNamespace)
                : @namespace;

            var separatedNamespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return (string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName().Name}/{separatedNamespace}{typeof(T).Name.Underscore()}"
                : $"{name}/{separatedNamespace}{typeof(T).Name.Underscore()}").ToLowerInvariant();
        }
    }
}