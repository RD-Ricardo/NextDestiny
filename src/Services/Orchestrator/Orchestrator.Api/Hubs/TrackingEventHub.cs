using Microsoft.AspNetCore.SignalR;

namespace Orchestrator.Api.Hubs
{
    public class TrackingEventHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var orderId = Context.GetHttpContext().Request.Query["orderId"];
            Console.Write(orderId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var orderId = Context.GetHttpContext().Request.Query["orderId"];
        }
    }
}
