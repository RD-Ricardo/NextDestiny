using Hotel.Application.Services;
using MassTransit;
using NextDestiny.Core.Shared.Events.Hotel;

namespace Hotel.Cancellation.Worker
{
    public class HotelBookingCancellationWorker : IConsumer<HotelBookingCancellationRequested>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public HotelBookingCancellationWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume(ConsumeContext<HotelBookingCancellationRequested> context)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var loggger = scope.ServiceProvider.GetRequiredService<ILogger<HotelBookingCancellationWorker>>();

            try
            {
                var bookingService = scope.ServiceProvider.GetRequiredService<IBookingService>();

                await bookingService.CancelAsync(context.Message);

                await context.ConsumeCompleted;
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "Error on booking hotel");
            }
        }
    }
}
