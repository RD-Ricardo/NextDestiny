namespace Flight.Application.Services
{
    public interface IFlightService
    {
        Task Booking(Guid orderId);
    }
}
