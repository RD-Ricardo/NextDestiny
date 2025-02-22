using NextDestiny.Core.DomainObjects;

namespace Order.Domain.Entities
{
    public class Customer : BaseEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
