using MongoDB.Driver;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;

namespace Catalog.Infrastructure.Persistence
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _productCollection;

        public ProductRepository(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("products");
        }

        public async Task CreateAsync(Product product, CancellationToken cancellationToken)
        {
            await _productCollection.InsertOneAsync(product, null, cancellationToken);
        }

        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _productCollection.Find(_ => true).ToListAsync(cancellationToken);
            return products;
        }
    }
}
