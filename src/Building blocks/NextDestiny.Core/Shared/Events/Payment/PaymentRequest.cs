namespace NextDestiny.Core.Shared.Events.Payment
{
    public class PaymentRequest
    {
        public PaymentRequest(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
