namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightReservedRequest
    {
        public Guid OrderId { get; set; }
        public int PassengerQuantity { get; set; }
        public DateTime Date  { get; set; }
        public string DepartureCity { get; set; } = null!;
        public string ArrivalCity { get; set; } = null!;
        public string DepartureAirport { get; set; } = null!;
        public string ArrivalAirport { get; set; } = null!;
    }
}
