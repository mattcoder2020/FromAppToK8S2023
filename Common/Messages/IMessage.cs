using Common.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Messages
{
    public interface IRejectedEvent : IEvent
    {
        string Reason { get; }
        string Code { get; }
    }
    public interface ICommand : IMessage
    {
        
    }
    public interface IEvent : IMessage
    { }
    public interface IMessage
    {
       ICorrelationContext Context { get; set; }
    }
}
