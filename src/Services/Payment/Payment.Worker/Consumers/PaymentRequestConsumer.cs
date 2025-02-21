using MassTransit;
using NextDestiny.Core.Shared.Events.Payment;

namespace Payment.Worker.Consumers
{
    public class PaymentRequestConsumer : IConsumer<PaymentRequest>
    {
        public Task Consume(ConsumeContext<PaymentRequest> context)
        {
            var paymentRequest = context.Message;
            Console.WriteLine($"Payment request received: {paymentRequest.Amount}");
            return Task.CompletedTask;
        }
    }
}
