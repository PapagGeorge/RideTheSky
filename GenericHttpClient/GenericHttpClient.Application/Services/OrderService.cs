using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain;

namespace GenericHttpClient.Application;

public class OrderService
{
    private readonly GenericHttpClient _client;

    public OrderService(IGenericHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("OrdersApi");
    }

    public async Task<Model.Order?> CreateOrderAsync(Model.CreateOrderRequest request)
    {
        return await _client.PostAsync<Model.CreateOrderRequest, Model.Order>("/orders", request);
    }

    public async Task<Model.Order?> GetOrderAsync(int orderId)
    {
        return await _client.GetAsync<Model.Order>($"/orders/{orderId}");
    }
}