namespace NextDestiny.Core.Shared.Events.Payment
{
    public class PaymentRequest
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime ProcessedAt { get; set; }
    }
}
