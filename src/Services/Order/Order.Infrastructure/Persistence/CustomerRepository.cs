using MongoDB.Driver;
using Order.Domain.Entities;
using Order.Domain.Repositories;

namespace Order.Infrastructure.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        private readonly FilterDefinitionBuilder<Customer> _filterBuilder;

        public CustomerRepository(IMongoDatabase database)
        {
            _filterBuilder = Builders<Customer>.Filter;
            _customerCollection = database.GetCollection<Customer>("customers");
        }

        public async Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _customerCollection.InsertOneAsync(customer, cancellationToken: cancellationToken);
            return customer;
        }

        public async Task<List<Customer>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _customerCollection.Find(_filterBuilder.Empty).ToListAsync(cancellationToken);
        }

        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _customerCollection.Find(c => c.Email == email).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _customerCollection.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
