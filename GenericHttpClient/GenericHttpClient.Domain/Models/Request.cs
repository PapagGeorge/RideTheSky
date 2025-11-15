namespace GenericHttpClient.Domain.Models;

public class Request<T>
{
    public string RequestId { get; set; } = Guid.NewGuid().ToString();
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public T Payload { get; set; }
}