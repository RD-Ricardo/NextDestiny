namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightBookingCompleted
    {
        public Guid OrderId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
