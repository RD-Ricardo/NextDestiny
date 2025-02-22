using Catalog.Domain.Entities;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product, CancellationToken cancellationToken);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
        Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
