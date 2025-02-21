namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightBookingCancelled
    {
        public Guid OrderId { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
