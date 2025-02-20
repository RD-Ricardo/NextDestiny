using Catalog.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Catalog.Application.Services.Interfaces;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto dto, CancellationToken cancellationToken)
        {
            var result = await _productService.CreateProductAsync(dto, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            var result = await _productService.GetProductsAsync(cancellationToken);
            return Ok(result);
        }
    }
}
