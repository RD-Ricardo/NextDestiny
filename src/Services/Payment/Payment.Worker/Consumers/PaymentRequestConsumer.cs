using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using NextDestiny.Core.Shared.Events.Payment;
using Payment.Application.Interfaces;

namespace Payment.Worker.Consumers
{
    public class PaymentRequestConsumer : IConsumer<PaymentRequest>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public PaymentRequestConsumer(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Consume(ConsumeContext<PaymentRequest> context)
        {
            var scope = _serviceScopeFactory.CreateScope();

            var loggger = scope.ServiceProvider.GetRequiredService<ILogger<PaymentRequestConsumer>>();

            try
            {
                var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

                await paymentService.ProcessAsync(context.Message.OrderId);

                await context.ConsumeCompleted;
            }
            catch (Exception ex)
            {
                loggger.LogError(ex, "Error on booking flight");
            }
        }
    }
}
