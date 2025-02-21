using NextDestiny.Core.DomainObjects;
using Payment.Domain.Enums;

namespace Payment.Domain.Entities
{
    public class Payment : BaseEntity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public PaymentStatus Status { get; set; }
        public DateTime DueAt { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public IList<PaymentHistory> Histories { get; set; } = [];
    }
}
