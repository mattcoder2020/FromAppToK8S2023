using System;

namespace Common.Messages.AzureServiceBus
{
    public class AzureBusBrokerFactory : IMessageBrokerFactory
    {
        public AzureBusBrokerFactory(IServiceProvider provider)
        {
            Provider = provider;
        }
        public IBusSubscriber Subscriber => new AzureBusSubscriber(Provider);

        public IBusPublisher Publisher => new AzureBusPublisher(Provider);

        private IServiceProvider Provider { get; set; }
    }
}
