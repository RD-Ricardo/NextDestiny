using Microsoft.AspNetCore.SignalR;

namespace Orchestrator.Api.Hubs
{
    public class TrackingEventHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var orderId = Context.GetHttpContext().Request.Query["orderId"];
            await Groups.AddToGroupAsync(Context.ConnectionId, orderId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var orderId = Context.GetHttpContext().Request.Query["orderId"];
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, orderId);
        }
    }
}
