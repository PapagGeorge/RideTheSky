using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
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
        bool usePolly = true,
        Action<IHttpClientBuilder>? configure = null)
    {
        var builder = _services.AddHttpClient(clientName, client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

        if (usePolly)
        {
            builder.AddPolicyHandler(GetRetryPolicy());
            builder.AddPolicyHandler(GetCircuitBreakerPolicy());
        }

        configure?.Invoke(builder);
    
        return this;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}