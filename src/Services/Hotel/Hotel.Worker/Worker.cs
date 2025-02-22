using MassTransit;
using NextDestiny.Core.Shared.Events.Hotel;

namespace Hotel.Worker
{
    public class Worker : IConsumer<HotelBookingRequested>
    {
        public Task Consume(ConsumeContext<HotelBookingRequested> context)
        {
            throw new NotImplementedException();
        }
    }

    public class WorkerDefinition : ConsumerDefinition<Worker>
    {
        public WorkerDefinition()
        {
            Endpoint(x => x.Name = "hotel-booking-requested");
        }
    }
}
