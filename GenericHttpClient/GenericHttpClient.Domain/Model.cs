namespace GenericHttpClient.Domain;

public class Model
{
    public record User(int Id, string Name, string Email);
    public record CreateUserRequest(string Name, string Email);
    public record Product(int Id, string Name, decimal Price);
    public record Order(int Id, int UserId, List<int> ProductIds, decimal Total);
    public record CreateOrderRequest(int UserId, List<int> ProductIds);
}