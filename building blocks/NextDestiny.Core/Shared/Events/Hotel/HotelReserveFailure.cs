namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelReserveFailure
    {
        public Guid OrderId { get; set; }
        public Guid ReserveId { get; set; }
    }
}
