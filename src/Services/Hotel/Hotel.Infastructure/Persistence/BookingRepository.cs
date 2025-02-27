using Hotel.Domain.Domain;
using Hotel.Domain.Repositoties;
using MongoDB.Driver;

namespace Hotel.Infastructure.Persistence
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IMongoCollection<Booking> _bookingCollection;

        public BookingRepository(IMongoDatabase database)
        {
            _bookingCollection = database.GetCollection<Booking>("bookings");
        }
        public async Task<Booking> CreateAsync(Booking booking)
        {
            await _bookingCollection.InsertOneAsync(booking);
            return booking;
        }

        public async Task<Booking> GetbyIdAsync(Guid Id)
        {
            return await _bookingCollection.Find(f => f.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<Booking> GetByOrderIdAsync(Guid orderId)
        {
            return await _bookingCollection.Find(f => f.OrderId == orderId).FirstOrDefaultAsync();
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
            await _bookingCollection.ReplaceOneAsync(f => f.Id == booking.Id, booking);
            return booking;
        }
    }
}
