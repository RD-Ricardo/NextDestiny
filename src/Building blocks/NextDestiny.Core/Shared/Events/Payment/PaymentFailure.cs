namespace NextDestiny.Core.Shared.Events.Payment
{
    public class PaymentFailure
    {
        public Guid Guid { get; set; }
        public string Reason { get; set; } = null!;
    }
}
