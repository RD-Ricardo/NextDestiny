namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightFailure
    {
        public Guid OrderId { get; set; }
        public string Reason { get; set; } = null!;
    }
}
