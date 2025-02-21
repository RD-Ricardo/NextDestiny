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
            var message = context.Message;

            var scope = _serviceScopeFactory.CreateScope();

            var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();

            await flightService.Booking(message.OrderId);

            Console.WriteLine($"Chegou mensagem {message.OrderId}");
        }
    }
}
