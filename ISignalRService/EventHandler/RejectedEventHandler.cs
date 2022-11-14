using Common.Handlers;
using Common.Messages;
using ISignalRService.Services;
using System;

using System.Threading.Tasks;

namespace ISignalRService.EventHandler
{
    public class RejectedEventHandler : IEventHandler<RejectedEvent>
    {
        private readonly IiSignalRService signalRService;

        public RejectedEventHandler(IiSignalRService signalRService)
        {
            this.signalRService = signalRService;
        }
     

        public async Task HandleAsync(RejectedEvent @event, ICorrelationContext context)
        {
            await signalRService.Publish(@event);
         }
    }
}
