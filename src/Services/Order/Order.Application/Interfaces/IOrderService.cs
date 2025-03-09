using System.Threading;
using Order.Application.Dtos;

namespace Order.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto request, CancellationToken cancellationToken);
        Task RetryAsync(Guid orderId, CancellationToken cancellationToken);
    }
}
