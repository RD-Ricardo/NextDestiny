namespace Flight.Domain.Entities
{
    public class FlightBookingEvent 
    {
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
