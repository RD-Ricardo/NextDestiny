namespace NextDestiny.Core.Shared.Events.Flight
{
    public class FlightBookingCancellationRequested
    {
        public FlightBookingCancellationRequested(Guid orderId)
        {
            OrderId = orderId;
        }
        public Guid OrderId { get; }
    }
}
