using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages
{
    public interface IMessageBrokerFactory
    {
     
        IBusSubscriber Subscriber { get;  }
        IBusPublisher Publisher { get; }

    }
}
