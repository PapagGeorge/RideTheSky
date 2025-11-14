using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.ValidateVat;
using Microsoft.Extensions.Logging;

namespace GenericHttpClient.Application.Services;

public class ValidationService : IValidationService
{
    private readonly IGenericHttpClient _client;
    private readonly ILogger<ValidationService> _logger;

    public ValidationService(IGenericHttpClientFactory httpClientFactory, ILogger<ValidationService> logger)
    {
        _client = httpClientFactory.CreateClient("ValidationApi");
        _logger = logger;
    }
    
    public async Task<VallidateVatResponse> VallidateVatAsync(VallidateVatRequest request)
    {
        return await _client.PostAsync<VallidateVatRequest, VallidateVatResponse>("/validate/vat", request);
    }

}