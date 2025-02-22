using Refit;

namespace Order.Application.Interfaces
{
    public interface ICatalogApi
    {
        [Get("/api/products/{productId}")]
        Task<ProductResponse> GetProduct(Guid productId);
    }

    public class ProductResponse 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
