using ConcurrentJobProcessor.Models;
using Microsoft.AspNetCore.SignalR;

namespace ConcurrentJobProcessor.Hubs
{
    public class ProductsHub : Hub
    {
        public async Task SendProductsUpdatedOnAdd(List<Products> updatedProducts)
        {
            if (updatedProducts.Count() == 0)
            {
                throw new HubException("An error occurred while sending the updated products. The array is empty.");
            }
            await Clients.All.SendAsync("ProductsUpdated", updatedProducts);
        }

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
