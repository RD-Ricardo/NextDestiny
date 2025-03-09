using Microsoft.AspNetCore.Mvc;
using Orchestrator.Api.Service;

namespace Orchestrator.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        public readonly ITrackingService _trackingService;

        public HomeController(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        [HttpGet]
        public IActionResult Get(Guid orderId)
        {
            _trackingService.SendTrackingEventAsync(orderId, "teste");
            return Ok("Orchestrator API");
        }
    }
}
