namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightBookingFailed
    {
        public Guid OrderId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
