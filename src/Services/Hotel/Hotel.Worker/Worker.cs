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
}
