using NextDestiny.Core.Amqp.Abstractions;
using NextDestiny.Core.Shared.Events.Order;
using NextDestiny.Core.Shared.Events.Payment;
using Order.Application.Interfaces;

namespace Order.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBusService _busService;
        public OrderService(IBusService busService)
        {
            _busService = busService;
        }

        public async Task CreateOrderAsync()
        {
            await _busService.Publish(new OrderSubmitted()
            {
                OrderId = Guid.NewGuid(),
            });

            Console.WriteLine("Order created");
        }
    }
}
