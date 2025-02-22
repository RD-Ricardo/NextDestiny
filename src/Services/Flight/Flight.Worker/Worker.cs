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

            var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();

            await flightService.Booking(context.Message);
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
