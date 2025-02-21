using Refit;

namespace Order.Application.Interfaces
{
    public interface ICatalogApi
    {
        [Get("/product/{productId}")]
        string GetProduct(Guid productId);
    }
}
