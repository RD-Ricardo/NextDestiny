using NextDestiny.Core.Amqp.Abstractions;
using NextDestiny.Core.Shared.Events.Flight;

namespace Flight.Application.Services
{
    public class FlightService : IFlightService
    {
        private readonly IBusService _busService;
        public FlightService(IBusService busService)
        {
            _busService = busService;
        }

        public async Task Booking(Guid orderId)
        {
            var @event = new FlightBookingCompleted
            {
                OrderId = orderId,
                Timestamp = DateTime.UtcNow 
            };

            await _busService.Publish(@event);

            Console.WriteLine($"Publicou mensagem {orderId}");
        }
    }
}
