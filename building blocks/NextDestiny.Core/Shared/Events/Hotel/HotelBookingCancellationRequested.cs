namespace NextDestiny.Core.Shared.Events.Hotel
{
    public class HotelBookingCancellationRequested
    {
        public Guid OrderId { get; set; }

        public HotelBookingCancellationRequested(Guid orderId)
        {
            OrderId = orderId;
        }
    }
}
