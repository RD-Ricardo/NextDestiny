namespace NextDestiny.Core.Shared.Events.Order
{
    public class OrderSubmitted
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerEmail { get; set; } = null!;
        public Guid ProductId { get; set; }
        public decimal Total { get; set; }
    }
}
