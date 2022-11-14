using OpenTracing;
using Polly;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Common.Handlers;

namespace Common.Messages
{
    public abstract class BaseSubscriber
    {
        private readonly ITracer _tracer;
        // private readonly IMessageBrokerFactory messageBrokerFactory;
        protected IBusPublisher _publisher;
        protected IServiceProvider _provider;
        protected ILogger _logger;
        protected int _retries;
        protected int _retryInterval;
        public BaseSubscriber(IServiceProvider provider)
        {
            //_logger = provider.GetService<ILogger<BusSubscriber>>();
            _tracer = provider.GetService<ITracer>();
            _provider = provider;
            _publisher = _provider.GetService<IBusPublisher>();
           // messageBrokerFactory = _provider.GetService<IMessageBrokerFactory>();

        }
        public virtual async Task BaseTryHandleAsync<TMessage>(TMessage message,
            CorrelationContext correlationContext,
            Func<Task> handle, Func<TMessage, Exception, IRejectedEvent> onError = null)
        {
            //var eventHandler = _provider.GetService<IEventHandler<TMessage>>();
            var currentRetry = 0;
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(3));

            var messageName = message.GetType().Name;

            await retryPolicy.ExecuteAsync<Task>(async () =>
            {
                try
                {
                    var scope = _tracer.BuildSpan("Handling " + messageName).AsChildOf(_tracer.ActiveSpan).StartActive();
                    using (scope)
                    {
                        var retryMessage = currentRetry == 0
                            ? string.Empty
                            : $"Retry: {currentRetry}'.";
                        _logger.LogInformation($"Handling a message: '{messageName}' " +
                                               $"with correlation id: '{correlationContext.Id}'. {retryMessage}");

                        await handle();

                        _logger.LogInformation($"Handled a message: '{messageName}' " +
                                               $"with correlation id: '{correlationContext.Id}'. {retryMessage}");

                        return Task.CompletedTask;
                    }
                }
               
                catch (Exception exception)
                {
                    currentRetry++;

                    _logger.LogError(exception, exception.Message);
                    if (exception.GetType().FullName.Contains("CustomizedException") && onError != null)
                    {
                        var rejectedEvent = onError(message, exception);
                        await _publisher.PublishAsync(rejectedEvent, correlationContext);
                        _logger.LogInformation($"Published a rejected event: '{rejectedEvent.GetType().Name}' " +
                                               $"for the message: '{messageName}' with correlation id: '{correlationContext.Id}'.");

                        return Task.CompletedTask;
                    }

                    throw new Exception($"Unable to handle a message: '{messageName}' " +
                                        $"with correlation id: '{correlationContext.Id}', " +
                                        $"retry {currentRetry - 1}/{_retries}...");
                }
            });
        }


    }
}
