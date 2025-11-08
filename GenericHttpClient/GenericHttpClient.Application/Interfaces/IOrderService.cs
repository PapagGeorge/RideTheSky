using GenericHttpClient.Domain;

namespace GenericHttpClient.Application.Interfaces;

public interface IOrderService
{
    Task<Model.Order?> CreateOrderAsync(Model.CreateOrderRequest request);
    Task<Model.Order?> GetOrderAsync(int orderId);
}