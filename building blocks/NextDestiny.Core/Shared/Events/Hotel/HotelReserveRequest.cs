namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelReserveRequest
    {
        public Guid OrderId { get; set; }
        public Guid HotelId { get; set; }
        public int NumberQuests { get; set; }
        public DateTime DateReserved { get; set; }
    }
}
