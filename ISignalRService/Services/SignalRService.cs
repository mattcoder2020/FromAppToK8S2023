using Common.Messages;
using ISignalRService.Hub;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISignalRService.Services
{
    public class SignalRService : IiSignalRService
    {
        IHubContext<MattHub> _hub;
        public SignalRService(IHubContext<MattHub> hub)
        {
            _hub = hub;
        }
        public async Task Publish(RejectedEvent evt)
        {
            await _hub.Clients.Groups(evt.Code).SendAsync("status_fail", new { id = evt.Code, cause = evt.Reason });

        }
    }
}
