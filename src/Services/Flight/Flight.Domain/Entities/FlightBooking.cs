using Flight.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using NextDestiny.Core.DomainObjects;

namespace Flight.Domain.Entities
{
    public class FlightBooking : BaseEntity
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid OrderId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Hour { get; set; }
        public FlightBookingStatus Status { get; set; }
        public string Origin { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string PassengerEmail { get; set; } = null!;
        public string? PassengerSeat { get; set; } = null!;
        public string? FlightExternalId { get; set; } = null!;
        public IList<FlightBookingEvent> Events { get; set; } = [];
    }
}
