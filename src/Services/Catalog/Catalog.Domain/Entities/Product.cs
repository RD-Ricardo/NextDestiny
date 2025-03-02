using Catalog.Domain.Enums;
using Catalog.Domain.ValueObjects;
using MongoDB.Bson.Serialization.Attributes;
using NextDestiny.Core.DomainObjects;

namespace Catalog.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public IList<string> Images { get; set; } = [];
        public IList<Feedback> Feedbacks { get; set; } = [];
        public Status Status { get; set; }
        public Location Location { get; set; } = null!;

        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid HotelId { get; set; }
    }
}
