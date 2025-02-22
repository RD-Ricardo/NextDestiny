using System.Threading;
using Order.Application.Dtos;

namespace Order.Application.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(OrderCreateDto request, CancellationToken cancellationToken);
    }
}
