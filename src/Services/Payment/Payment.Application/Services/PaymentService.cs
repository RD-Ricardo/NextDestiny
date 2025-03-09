using NextDestiny.Core.Amqp.Abstractions;
using NextDestiny.Core.Shared.Events.Payment;
using Payment.Application.Interfaces;

namespace Payment.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBusService _busService;
        public PaymentService(IBusService busService)
        {
            _busService = busService;
        }

        public async Task ProcessAsync(Guid orderId)
        {
            var random = new Random();

            var paymentResult =  random.Next(0, 1);

            if (paymentResult == 0)
            {
                await _busService.PublishAsync(new PaymentProcessed(orderId));
            }
            else
            {
                await _busService.PublishAsync(new PaymentFailed()
                {
                    OrderId = orderId,
                    Timestamp = DateTime.UtcNow
                });
            }
        }
    }
}
