namespace Order.Domain.Repositories
{
    public interface IOrderRepository
    {
        Task CreateAsync(Entities.Order order, CancellationToken cancellationToken);
        Task<Entities.Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Entities.Order>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<Entities.Order>> GettAllByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
    }
}
