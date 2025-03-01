using Hotel.Application.Services;
using MassTransit;
using NextDestiny.Core.Shared.Events.Hotel;

namespace Hotel.Worker
{
    public class HotelWorker : IConsumer<HotelBookingRequested>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HotelWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume(ConsumeContext<HotelBookingRequested> context)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var loggger = scope.ServiceProvider.GetRequiredService<ILogger<HotelWorker>>();

            try
            {
                var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();

                await bookingService.BookingAsync(context.Message);

                await context.ConsumeCompleted;
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "Error on request booking hotel");
            }
        }
    }

    public class WorkerDefinition : ConsumerDefinition<HotelWorker>
    {
        public WorkerDefinition()
        {
            Endpoint(x => x.Name = "hotel-booking-requested");
        }
    }
}
