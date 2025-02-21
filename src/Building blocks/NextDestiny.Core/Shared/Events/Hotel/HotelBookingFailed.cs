namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelBookingFailed
    {
        public Guid OrderId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
