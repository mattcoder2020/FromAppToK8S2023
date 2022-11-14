using Common.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages.RabbitMQ
{
    public class RabbitMQBrokerFactory : IMessageBrokerFactory
    {
        IBusSubscriber subscriber;
        IBusPublisher publisher;
        readonly IBusClient client;
        readonly RabbitMqOptions options;
        //public RabbitMQBrokerFactory(IServiceProvider provider, IBusClient client, RabbitMqOptions options)
        //{
        //   // Subscriber = new BusSubscriber(provider.GetService);
        //    Publisher = new BusPublisher(client, options);
        //}

        public RabbitMQBrokerFactory(IServiceProvider provider)
        {
            
            client = provider.GetService<IBusClient>();
            options = provider.GetService<RabbitMqOptions>();
            Publisher = new BusPublisher(client, options);
            Subscriber = new BusSubscriber(provider);
        }
        public IBusSubscriber Subscriber
        {
            get { return subscriber; }
            private set { subscriber = value; }
        }

        public IBusPublisher Publisher
        {
            get { return publisher; }
            private set { publisher = value; }
        }
    }
}
