using GenericHttpClient.Application.Configuration;
using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.GetEmailReputation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GenericHttpClient.Application.Services;

public class AbstractService : IAbstractService
{
    private readonly IGenericHttpClient _client;
    private readonly ILogger<AbstractService> _logger;
    private readonly string _apiKey;

    public AbstractService(IGenericHttpClientFactory httpClientFactory,
        ILogger<AbstractService> logger, IOptions<EmailReputationConfig> config)
    {
        _client = httpClientFactory.CreateClient("EmailReputationApi");
        _logger = logger;
        _apiKey = config.Value.ApiKey;
    }

    public async Task<GetEmailReputationResponse> GetEmailReputationAsync(GetEmailReputationRequest request)
    {
        var endpoint = $"/v1?api_key={_apiKey}&email={request.Email}";
        return await _client.GetAsync<GetEmailReputationResponse>(endpoint);
    }

}
