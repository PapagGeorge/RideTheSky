using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain;

namespace GenericHttpClient.Application;

public class ProductService
{
    private readonly GenericHttpClient _client;

    public ProductService(IGenericHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("ProductsApi");
    }

    public async Task<Model.Product?> GetProductAsync(int productId)
    {
        return await _client.GetAsync<Model.Product>($"/products/{productId}");
    }

    public async Task<List<Model.Product>?> GetAllProductsAsync()
    {
        return await _client.GetAsync<List<Model.Product>>("/products");
    }
}