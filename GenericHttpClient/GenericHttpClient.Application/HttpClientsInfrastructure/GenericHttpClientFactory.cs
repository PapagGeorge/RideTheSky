using GenericHttpClient.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace GenericHttpClient.Application.HttpClientsInfrastructure;

public class GenericHttpClientFactory : IGenericHttpClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GenericHttpClient> _logger;

    public GenericHttpClientFactory(
        IHttpClientFactory httpClientFactory,
        ILogger<GenericHttpClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public GenericHttpClient CreateClient(string name)
    {
        var httpClient = _httpClientFactory.CreateClient(name);
        return new GenericHttpClient(httpClient, _logger);
    }
}