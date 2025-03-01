using Flight.Application.Services;
using MassTransit;
using NextDestiny.Core.Shared.Events.Flight;

namespace Flight.Worker
{
    public class FlightBookingWorker : IConsumer<FlightBookingRequested>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public FlightBookingWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume(ConsumeContext<FlightBookingRequested> context)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var loggger = scope.ServiceProvider.GetRequiredService<ILogger<FlightBookingWorker>>();

            try
            {
                var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();

                await flightService.BookingAsync(context.Message);

                await context.ConsumeCompleted;
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "Error on booking flight");
            }
        }
    }

    public class WorkerDefinition : ConsumerDefinition<FlightBookingWorker>
    {
        public WorkerDefinition()
        {
            Endpoint(x => x.Name = "flight-booking-requested");
        }
    }
}
