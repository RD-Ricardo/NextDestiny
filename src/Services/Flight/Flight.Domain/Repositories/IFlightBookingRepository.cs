using Flight.Domain.Entities;

namespace Flight.Domain.Repositories
{
    public interface IFlightBookingRepository
    {
        Task<FlightBooking> GetbyIdAsync(Guid Id);
        Task<FlightBooking> CreateAsync(FlightBooking flightBooking);
        Task<FlightBooking> UpdateAsync(FlightBooking flightBooking);
        Task<FlightBooking> GetbyOrderIdAsync(Guid orderId);
    }
}
