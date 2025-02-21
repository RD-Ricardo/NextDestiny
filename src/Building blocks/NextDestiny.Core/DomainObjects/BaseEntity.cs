using MongoDB.Bson.Serialization.Attributes;

namespace NextDestiny.Core.DomainObjects
{
    public abstract class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = null;
        
        public BaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
