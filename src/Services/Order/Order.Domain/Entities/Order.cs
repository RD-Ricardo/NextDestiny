using Order.Domain.Enums;
using NextDestiny.Core.DomainObjects;
using Order.Domain.ValueObjects;

namespace Order.Domain.Entities
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public OrderStatus Status { get; set; }
        public Customer Customer { get; set; } = null!;
        public IList<OrderItem> Items { get; set; } = [];
        public decimal Total { get; set; }
    }
}
