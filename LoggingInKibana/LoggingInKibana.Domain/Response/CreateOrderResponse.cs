namespace LoggingInKibana.Domain.Response;

public class CreateOrderResponse
{
    public string OrderId { get; set; } = Guid.NewGuid().ToString();
    public string Status { get; set; } = "Created";
}