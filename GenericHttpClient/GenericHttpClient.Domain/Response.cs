namespace GenericHttpClient.Domain.Responses;

public class Response<T>
{
    public string RequestId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Success"; // or "Error"
    public string Message { get; set; } = string.Empty;
    public int? StatusCode { get; set; }
    public T? Data { get; set; }
    public double? ExecutionTimeMs { get; set; }
    public List<string>? Errors { get; set; }
}