using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Reflection;

namespace Common.Messages.AzureServiceBus
{
    public class AzureBusPublisher : IBusPublisher
    {
        private readonly string azureConnection;
        private TopicClient client;
        private readonly string app;
        public AzureBusPublisher(IServiceProvider provider) 
        {

            azureConnection = provider.GetService<AzureBusOptions>().connection;
            //app = provider.GetService<AzureBusOptions>().connection;
        }
        public async Task PublishAsync<TEvent>(TEvent @event, ICorrelationContext context) where TEvent : IEvent
        {
            var json = JsonConvert.SerializeObject(@event);
            var jsonContext = JsonConvert.SerializeObject(context);
            var message = new Message(Encoding.UTF8.GetBytes(json));

            client = new TopicClient( azureConnection, GetAttribute(@event));
            if (context != null) message.UserProperties.Add("context", jsonContext);
            await client.SendAsync(message);
           
        }

        private string GetAttribute<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var @namespace = @event.GetType().GetCustomAttribute<MessageNamespaceAttribute>()?.Namespace;
            return @namespace;
        }

       

        public Task SendAsync<TCommand>(TCommand command, ICorrelationContext context) where TCommand : ICommand
        {
            throw new NotImplementedException();
        }
    }
}
