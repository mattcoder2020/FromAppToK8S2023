using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ISignalRService.Hub
{
    public class MattHub: Microsoft.AspNetCore.SignalR.Hub
    {
        public async void Initialize(string productid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, productid);
            await ConnectAync();
        }

        private async Task ConnectAync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("connected");
        }

        private async Task DisconnectAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("disconnected");
        }
    }
}
