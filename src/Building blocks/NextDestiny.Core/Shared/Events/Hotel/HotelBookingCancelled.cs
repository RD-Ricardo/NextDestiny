namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelBookingCancelled
    {
        public HotelBookingCancelled(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; set; }
    }
}
