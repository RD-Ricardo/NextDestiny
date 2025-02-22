using MongoDB.Driver;
using Order.Domain.Repositories;

namespace Order.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Domain.Entities.Order> _orderCollection;

        private readonly FilterDefinitionBuilder<Domain.Entities.Order> _filterBuilder;

        public OrderRepository(IMongoDatabase database)
        {
            _filterBuilder = Builders<Domain.Entities.Order>.Filter;
            _orderCollection = database.GetCollection<Domain.Entities.Order>("orders");
        }

        public Task CreateAsync(Domain.Entities.Order order, CancellationToken cancellationToken)
        {
            return _orderCollection.InsertOneAsync(order, cancellationToken: cancellationToken);
        }

        public async Task<List<Domain.Entities.Order>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _orderCollection.Find(FilterDefinition<Domain.Entities.Order>.Empty).ToListAsync(cancellationToken);
        }

        public async Task<Domain.Entities.Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _orderCollection.Find(o => o.Id == id).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Domain.Entities.Order>> GettAllByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            var filter = _filterBuilder.Eq(o => o.Customer.Id, customerId);
            return await _orderCollection.Find(filter).ToListAsync(cancellationToken);
        }
    }
}
