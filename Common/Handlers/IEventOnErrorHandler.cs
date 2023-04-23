using Common.Messages;
using System;

namespace Common.Handlers
{
    public interface IEventOnErrorHandler<TEvent> where TEvent : IEvent
    {
        IRejectedEvent OnError(TEvent ev, Exception exception);
    }
}