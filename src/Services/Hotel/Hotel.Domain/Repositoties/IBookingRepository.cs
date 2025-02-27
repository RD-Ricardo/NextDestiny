using Hotel.Domain.Domain;

namespace Hotel.Domain.Repositoties
{
    public interface IBookingRepository
    {
        Task<Booking> GetbyIdAsync(Guid Id);
        Task<Booking> CreateAsync(Booking booking);
        Task<Booking> UpdateAsync(Booking booking);
        Task<Booking> GetByOrderIdAsync(Guid orderId);
    }
}
