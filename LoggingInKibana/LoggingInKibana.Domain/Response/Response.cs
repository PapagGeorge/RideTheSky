namespace LoggingInKibana.Domain.Response;

public class Response<T>
{
    public string TransactionId { get; set; } = Guid.NewGuid().ToString();
    public string ServiceName { get; set; } = "MockService";
    public T Payload { get; set; } = default!;
    public int StatusCode { get; set; } = 200;
    public string? ErrorMessage { get; set; }
}