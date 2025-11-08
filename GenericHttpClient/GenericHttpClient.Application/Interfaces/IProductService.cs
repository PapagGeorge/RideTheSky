using GenericHttpClient.Domain;

namespace GenericHttpClient.Application.Interfaces;

public interface IProductService
{
    Task<Model.Product?> GetProductAsync(int productId);
    Task<List<Model.Product>?> GetAllProductsAsync();
}