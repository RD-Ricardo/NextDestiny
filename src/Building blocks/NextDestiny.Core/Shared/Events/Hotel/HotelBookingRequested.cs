namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelBookingRequested
    {
        public Guid OrderId { get; }
        public string CustomerEmail { get; set; }

        public HotelBookingRequested(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
