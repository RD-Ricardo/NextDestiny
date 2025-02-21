namespace NextDestiny.Core.Shared.Events.Order
{
    public class OrderCompleted
    {
        public OrderCompleted(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
