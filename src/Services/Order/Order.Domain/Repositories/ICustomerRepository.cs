namespace Order.Domain.Repositories
{
    public interface ICustomerRepository 
    {
        Task<Entities.Customer> CreateAsync(Entities.Customer customer, CancellationToken cancellationToken);
        Task<Entities.Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Entities.Customer>> GetAllAsync(CancellationToken cancellationToken);
        Task<Entities.Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
