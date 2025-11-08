using Microsoft.Extensions.DependencyInjection;
namespace GenericHttpClient.Application;

public class GenericHttpClientBuilder
{
    private readonly IServiceCollection _services;

    public GenericHttpClientBuilder(IServiceCollection services)
    {
        _services = services;
    }

    public GenericHttpClientBuilder AddClient(
        string clientName,
        string baseAddress,
        Action<IHttpClientBuilder>? configure = null)
    {
        var builder = _services.AddHttpClient(clientName, client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

        configure?.Invoke(builder);
        return this;
    }
}