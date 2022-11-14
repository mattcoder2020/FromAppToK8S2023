using Common.Messages;
using System;
using System.Threading.Tasks;

namespace Common.Messages
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, Exception, IRejectedEvent> onError = null)
            where TCommand : ICommand;

        Task<IBusSubscriber> SubscribeEvent<TEvent>(string @namespace = null, string queueName = null,
            Func<TEvent, Exception, IRejectedEvent> onError = null)
            where TEvent : IEvent;
    }
}
