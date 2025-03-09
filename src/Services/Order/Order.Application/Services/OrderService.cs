using Order.Application.Interfaces;
using NextDestiny.Core.Amqp.Abstractions;
using Order.Application.Dtos;
using Order.Domain.Repositories;
using Order.Domain.ValueObjects;
using Order.Domain.Enums;
using NextDestiny.Core.Shared.Events.Order;

namespace Order.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBusService _busService;

        private readonly IOrderRepository _orderRepository;

        private readonly ICustomerRepository _customerRepository;

        private readonly ICatalogApi _catalogApi;

        public OrderService(IBusService busService,
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            ICatalogApi catalogApi)
        {
            _busService = busService;
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _catalogApi = catalogApi;
        }

        public async Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto request, CancellationToken cancellationToken)
        {
           var order = await MapOrder(request, cancellationToken);

            foreach (var item in order.Items)
            {
                var product = await _catalogApi.GetProduct(item.ProductId) 
                    ?? throw new Exception($"Produto não encontrado");
                
                item.Name = product.Name;
                item.Description = product.Description;
                item.Price = product.Price;
            }   
            
            await _orderRepository.CreateAsync(order, cancellationToken);

            await _busService.PublishAsync(new OrderSubmitted
            {
                OrderId = order.Id,
                CustomerId = order.Customer.Id,
                Total = order.Items.Sum(x => x.Price * x.Quantity),
                ProductId = order.Items.FirstOrDefault()?.ProductId ?? Guid.Empty
            });

            return new OrderResponseDto
            {
                Id = order.Id,
            };
        }

        public async Task RetryAsync(Guid orderId, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(orderId, cancellationToken);
            
            if (order is null)
            {
                throw new Exception("Pedido não encontrado");
            }
            
            await _busService.PublishAsync(new OrderSubmitted
            {
                OrderId = order.Id,
                CustomerId = order.Customer.Id,
                Total = order.Items.Sum(x => x.Price * x.Quantity),
                ProductId = order.Items.FirstOrDefault()?.ProductId ?? Guid.Empty
            });
        }

        private async Task<Domain.Entities.Order> MapOrder(OrderCreateDto request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByEmailAsync(request.Customer.Email, cancellationToken);

            if (customer is null)
            {
                customer = new Domain.Entities.Customer
                {
                    Email = request.Customer.Email,
                    Name = request.Customer.Name
                };

                await _customerRepository.CreateAsync(customer, cancellationToken);
            }

            return new Domain.Entities.Order
            {
                Status = OrderStatus.Pending,
                Customer = customer,
                Items = request.Items.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity
                }).ToList()
            };
        }
    }
}
