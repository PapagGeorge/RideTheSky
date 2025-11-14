namespace GenericHttpClient.Application.Configuration;

public class HttpClientOptions
{
    public string BaseAddress { get; set; } = string.Empty;
    public Dictionary<string, string> DefaultHeaders { get; set; } = new();
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}