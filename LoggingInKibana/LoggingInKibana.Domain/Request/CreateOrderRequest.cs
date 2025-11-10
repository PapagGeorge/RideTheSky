namespace LoggingInKibana.Domain.Request;

public class CreateOrderRequest
{
    public string ProductId { get; set; } = "";
    public int Quantity { get; set; }
}