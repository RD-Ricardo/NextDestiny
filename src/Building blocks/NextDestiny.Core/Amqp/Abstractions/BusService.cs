using MassTransit;

namespace NextDestiny.Core.Amqp.Abstractions
{
    public class BusService : IBusService
    {
        private readonly IBus _bus;

        public BusService(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync<T>(T message) where T : class
        {
            await _bus.Publish(message);
        }
    }
}
