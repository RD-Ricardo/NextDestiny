namespace NextDestiny.Core.Shared.Events.Payment
{
    public class PaymentProcessed
    {
        public Guid OrderId { get; set; }

        public DateTime Timestamp { get; set; }

        public PaymentProcessed(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
