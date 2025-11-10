namespace LoggingInKibana.Domain.Response;

public class GetOrderResponse
{
    public string OrderId { get; set; } = "";
    public string ProductId { get; set; } = "";
    public int Quantity { get; set; }
    public string Status { get; set; } = "";
}