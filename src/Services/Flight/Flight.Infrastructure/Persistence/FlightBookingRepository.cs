using Flight.Domain.Entities;
using Flight.Domain.Repositories;
using MongoDB.Driver;

namespace Flight.Infrastructure.Persistence
{
    public class FlightBookingRepository : IFlightBookingRepository
    {
        private readonly IMongoCollection<FlightBooking> _flightBookingCollection;

        public FlightBookingRepository(IMongoDatabase database)
        {
            _flightBookingCollection = database.GetCollection<FlightBooking>("flightBookings");
        }

        public async Task<FlightBooking> CreateAsync(FlightBooking flightBooking)
        {
            await _flightBookingCollection.InsertOneAsync(flightBooking);
            return flightBooking;
        }

        public async Task<FlightBooking> GetbyIdAsync(Guid Id)
        {
            return await _flightBookingCollection.Find(f => f.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<FlightBooking> GetbyOrderIdAsync(Guid orderId)
        {
            return await _flightBookingCollection.Find(f => f.OrderId == orderId).FirstOrDefaultAsync();
        }

        public async Task<FlightBooking> UpdateAsync(FlightBooking flightBooking)
        {
            await _flightBookingCollection.ReplaceOneAsync(f => f.Id == flightBooking.Id, flightBooking);
            return flightBooking;
        }
    }
}
