using MassTransit;
using NextDestiny.Core.Shared.Events.Flight;

namespace Flight.Cancellation.Worker
{
    public class Worker : IConsumer<FlightBookingCancellationRequested>
    {
        public Task Consume(ConsumeContext<FlightBookingCancellationRequested> context)
        {
            throw new NotImplementedException();
        }
    }
}
