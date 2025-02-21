using MassTransit;
using NextDestiny.Core.Shared.Events.Hotel;

namespace Hotel.Cancellation.Worker
{
    public class Worker : IConsumer<HotelBookingCancellationRequested>
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<HotelBookingCancellationRequested> context)
        {
            throw new NotImplementedException();
        }
    }
}
