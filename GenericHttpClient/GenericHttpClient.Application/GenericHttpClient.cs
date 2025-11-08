using System.Text;
using System.Text.Json;
using GenericHttpClient.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace GenericHttpClient.Application;

public class GenericHttpClient : IGenericHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<GenericHttpClient> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public GenericHttpClient(
        HttpClient httpClient,
        ILogger<GenericHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public async Task<TResponse?> GetAsync<TResponse>(string endpoint, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("GET request to {Endpoint}", endpoint);
            var response = await _httpClient.GetAsync(endpoint, ct);
            return await HandleResponse<TResponse>(response, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GET request to {Endpoint}", endpoint);
            throw;
        }
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string endpoint, TRequest request, CancellationToken ct = default)
    {
        return await SendWithBodyAsync<TRequest, TResponse>(HttpMethod.Post, endpoint, request, ct);
    }

    public async Task<TResponse?> PutAsync<TRequest, TResponse>(
        string endpoint, TRequest request, CancellationToken ct = default)
    {
        return await SendWithBodyAsync<TRequest, TResponse>(HttpMethod.Put, endpoint, request, ct);
    }

    public async Task<TResponse?> PatchAsync<TRequest, TResponse>(
        string endpoint, TRequest request, CancellationToken ct = default)
    {
        return await SendWithBodyAsync<TRequest, TResponse>(HttpMethod.Patch, endpoint, request, ct);
    }

    public async Task<bool> DeleteAsync(string endpoint, CancellationToken ct = default)
    {
        try
        {
            _logger.LogInformation("DELETE request to {Endpoint}", endpoint);
            var response = await _httpClient.DeleteAsync(endpoint, ct);
            response.EnsureSuccessStatusCode();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DELETE request to {Endpoint}", endpoint);
            throw;
        }
    }

    private async Task<TResponse?> SendWithBodyAsync<TRequest, TResponse>(
        HttpMethod method, string endpoint, TRequest request, CancellationToken ct)
    {
        try
        {
            _logger.LogInformation("{Method} request to {Endpoint}", method, endpoint);
            var json = JsonSerializer.Serialize(request, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpRequest = new HttpRequestMessage(method, endpoint) { Content = content };
            var response = await _httpClient.SendAsync(httpRequest, ct);
            return await HandleResponse<TResponse>(response, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in {Method} request to {Endpoint}", method, endpoint);
            throw;
        }
    }

    private async Task<TResponse?> HandleResponse<TResponse>(
        HttpResponseMessage response, CancellationToken ct)
    {
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(ct);
            _logger.LogWarning("Request failed with status {StatusCode}: {Content}", 
                response.StatusCode, errorContent);
            response.EnsureSuccessStatusCode();
        }
        var contentStream = await response.Content.ReadAsStreamAsync(ct);
        return await JsonSerializer.DeserializeAsync<TResponse>(contentStream, _jsonOptions, ct);
    }
}