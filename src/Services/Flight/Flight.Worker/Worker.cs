using Flight.Application.Services;
using MassTransit;
using NextDestiny.Core.Shared.Events.Flight;

namespace Flight.Worker
{
    public class Worker : IConsumer<FlightBookingRequested>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Worker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume(ConsumeContext<FlightBookingRequested> context)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var loggger = scope.ServiceProvider.GetRequiredService<ILogger<Worker>>();

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

    public class WorkerDefinition : ConsumerDefinition<Worker>
    {
        public WorkerDefinition()
        {
            Endpoint(x => x.Name = "flight-booking-requested");
        }
    }
}
