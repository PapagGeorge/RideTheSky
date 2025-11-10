namespace LoggingInKibana.Domain.Request;

public class Request<T>
{
    public string TransactionId { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = "user-123"; // mock
    public string ServiceName { get; set; } = "MockService"; // mock
    public T Payload { get; set; } = default!;
}