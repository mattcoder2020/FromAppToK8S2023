using Common.Handlers;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Messages.AzureServiceBus
{
    public class AzureBusSubscriber : BaseSubscriber, IBusSubscriber 
    {
        private readonly string azureConnection;
        private readonly IServiceProvider provider;

        public AzureBusSubscriber(IServiceProvider provider):base(provider)
        {
            azureConnection = provider.GetService<AzureBusOptions>().connection;
            this.provider = provider;
            _logger = provider.GetService<ILogger<AzureBusSubscriber>>();
        }
        public IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null, 
            Func<TCommand, Exception, IRejectedEvent> onError = null) where TCommand : ICommand
        {
            throw new NotImplementedException();
        }

        public async Task<IBusSubscriber> SubscribeEvent<TEvent>(string @namespace = null, string queueName = null, 
            Func<TEvent, Exception, IRejectedEvent> onError = null) where TEvent : IEvent
        {
            var client = new ManagementClient(azureConnection);
            if (!await client.SubscriptionExistsAsync(@namespace, queueName, CancellationToken.None))
            {
                await client.CreateSubscriptionAsync(@namespace, queueName, CancellationToken.None);

            }
            SubscriptionClient subscriptionClient = new SubscriptionClient(azureConnection, @namespace, queueName, ReceiveMode.ReceiveAndDelete, RetryPolicy.Default);

            var options = new MessageHandlerOptions(e =>
            {
               
                return Task.CompletedTask;
            })
            {
                AutoComplete = false,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(1)
            };

            subscriptionClient.RegisterMessageHandler(
                async (message, token) => 
                {
                    TEvent @event = default(TEvent);
                    try
                    {
                        // Get message
                        var data = Encoding.UTF8.GetString(message.Body);
                        var propertydata = message.UserProperties["context"] as string;
                        @event = JsonConvert.DeserializeObject<TEvent>(data);
                        CorrelationContext correlationContext = JsonConvert.DeserializeObject<CorrelationContext>(propertydata);
                        
                        var eventHandler = provider.GetService<IEventHandler<TEvent>>();

                        //return await TryHandleAsync(@event, correlationContext,
                        await BaseTryHandleAsync(@event, correlationContext,
                            () => eventHandler.HandleAsync(@event, correlationContext), onError);

                    }
                    catch (Exception ex)
                    {
                        //await client.DeadLetterAsync(message.SystemProperties.LockToken);
                        onError(@event, ex);
                    }
                }, options);
            return this;
        }

      
    }
}
