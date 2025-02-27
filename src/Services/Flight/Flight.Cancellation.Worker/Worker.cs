using Flight.Application.Services;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Shared.Events.Flight;

namespace Flight.Cancellation.Worker
{
    public class Worker : IConsumer<FlightBookingCancellationRequested>
    {

        private readonly IServiceScopeFactory _serviceScopeFactory;
        public Worker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume(ConsumeContext<FlightBookingCancellationRequested> context)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var loggger = scope.ServiceProvider.GetRequiredService<ILogger<Worker>>();

            try
            {
                var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();

                await flightService.CancelAsync(context.Message);

                await context.ConsumeCompleted;
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "Error on cancel flight booking");
            }
        }
    }
}
