using ConcurrentJobProcessor.Models;
using Microsoft.AspNetCore.SignalR;

namespace ConcurrentJobProcessor.Hubs
{
    public class ProductsHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessage", "Server", "Welcome to SignalR Chat!");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Server", "A user has disconnected.");
        }
    }
}
