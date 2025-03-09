using Microsoft.AspNetCore.SignalR;
using Orchestrator.Api.Hubs;

namespace Orchestrator.Api.Service
{

    public interface ITrackingService
    {
        Task SendTrackingEventAsync(Guid orderId, string message);

        Task CreateGroupAsync(Guid orderId);
    }

    public class TrackingService : ITrackingService
    {
        private readonly IHubContext<TrackingEventHub> _hubContext;

        public TrackingService(IHubContext<TrackingEventHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task CreateGroupAsync(Guid orderId)
        {
            //await _hubContext.Groups.AddToGroupAsync(orderId.ToString(), orderId.ToString());
            await _hubContext.Clients.All.SendAsync("ReceiveTrackingEvent", orderId, "Comecou");
        }

        public async Task SendTrackingEventAsync(Guid orderId, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveTrackingEvent", orderId, message);
            //await _hubContext.Clients.Group(orderId.ToString()).SendAsync("ReceiveTrackingEvent", message);
        }
    }
}
