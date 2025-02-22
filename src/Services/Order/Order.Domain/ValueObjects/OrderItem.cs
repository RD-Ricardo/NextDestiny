using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Order.Domain.ValueObjects
{
    public class OrderItem
    {
        [BsonRepresentation(BsonType.String)]
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
