using Catalog.Application.Dtos;

namespace Catalog.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken);
        Task<ProductDto> GetProductAsync(Guid id, CancellationToken cancellationToken);
        Task<ProductDto> CreateProductAsync(ProductCreateDto createProductDto, CancellationToken cancellationToken);
    }
}
