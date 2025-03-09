using Microsoft.AspNetCore.Mvc;
using Order.Application.Dtos;
using Order.Application.Interfaces;

namespace Order.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _orderService.CreateOrderAsync(dto, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{orderId}/retry")]
        public async Task<IActionResult> Retry(Guid orderId, CancellationToken cancellationToken)
        {
            await _orderService.RetryAsync(orderId, cancellationToken);
            return Ok();
        }
    }
}
