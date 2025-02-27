namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelBookingFailed
    {
        public HotelBookingFailed(Guid orderId)
        {
            OrderId = orderId;
            Timestamp = DateTime.UtcNow;
        }

        public Guid OrderId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
