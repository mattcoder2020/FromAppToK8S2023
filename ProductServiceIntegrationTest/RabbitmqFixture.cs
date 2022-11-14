using Common;
using Common.Messages;
using Common.RabbitMQ;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Configuration;
using RawRabbit.Enrichers.MessageContext;
using RawRabbit.Instantiation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.IntegrationTest
{
    public class RabbitMqFixture
    {
        private readonly RawRabbit.Instantiation.Disposable.BusClient _client;
        private readonly IConfiguration config;
        private readonly RabbitMqOptions option;
        bool _disposed = false;

        public RabbitMqFixture()
        {
            var builder = new ConfigurationBuilder();
            config = builder.AddJsonFile("appsettings.json").Build();
            option = config.GetOptions<RabbitMqOptions>("rabbitMq");

            _client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions()
            {
                ClientConfiguration = new RawRabbitConfiguration
                {
                    Hostnames = option.Hostnames , // localhost
                    VirtualHost = "/",
                    Port = option.Port,
                    Username = option.Username,
                    Password = option.Password,
                },
                DependencyInjection = ioc =>
                {
                    ioc.AddSingleton<INamingConventions>(new RabbitMqNamingConventions(option.Namespace));
                },
                Plugins = p => p
                    .UseAttributeRouting()
                    .UseRetryLater()
                    .UseMessageContext<CorrelationContext>()
                    .UseContextForwarding()
            });
        }

        public Task PublishAsync<TMessage>(TMessage message, string @namespace = null) where TMessage : class
            => _client.PublishAsync(message, ctx =>
                ctx.UseMessageContext(CorrelationContext.Empty).UsePublishConfiguration(p => p.WithRoutingKey(GetRoutingKey(@message, @namespace))));

        public async Task<TaskCompletionSource<TEvent>> SubscribeAndGetAsync<TEvent>(
            Func<TEvent, TaskCompletionSource<TEvent>, Task> onMessageReceived, string queueName ) where TEvent : IEvent
        {
            var taskCompletionSource = new TaskCompletionSource<TEvent>();
            var guid = Guid.NewGuid().ToString();

            await _client.SubscribeAsync<TEvent>(async (@event) =>  await onMessageReceived(@event, taskCompletionSource)                   
                ,
            ctx => ctx.UseSubscribeConfiguration(cfg =>
                  cfg.FromDeclaredQueue(q => q.WithName(GetQueueName<TEvent>(option.Namespace, queueName)))));
            return taskCompletionSource;
        }

        private string GetQueueName<T>(string @namespace = null, string name = null)
        {
            @namespace = string.IsNullOrWhiteSpace(@namespace)
                ? (string.IsNullOrWhiteSpace(option.Namespace) ? string.Empty : option.Namespace)
                : @namespace;

            var separatedNamespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return (string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName().Name}/{separatedNamespace}{typeof(T).Name.Underscore()}"
                : $"{name}/{separatedNamespace}{typeof(T).Name.Underscore()}").ToLowerInvariant();
        }

        private string GetRoutingKey<T>(T message, string @namespace = null)
        {
            @namespace = @namespace ?? option.Namespace;
            @namespace = string.IsNullOrWhiteSpace(@namespace) ? string.Empty : $"{@namespace}.";

            return $"{@namespace}{typeof(T).Name.Underscore()}".ToLowerInvariant();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _client.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
