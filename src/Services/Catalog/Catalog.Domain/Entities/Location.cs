using NextDestiny.Core.DomainObjects;

namespace Catalog.Domain.Entities
{
    public class Location : BaseEntity
    {
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
    }
}
