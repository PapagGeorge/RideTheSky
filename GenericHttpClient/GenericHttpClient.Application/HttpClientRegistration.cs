using Microsoft.Extensions.DependencyInjection;

namespace GenericHttpClient.Application;

public class HttpClientRegistration
{
    public string ClientName { get; set; } =  string.Empty;
    public string BaseAddress { get; set; } =  string.Empty;
    public Action<IHttpClientBuilder>? Configure = null;
}