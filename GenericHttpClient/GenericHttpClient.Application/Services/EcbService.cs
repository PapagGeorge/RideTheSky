using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.GetDailyRates;

namespace GenericHttpClient.Application.Services;

public class EcbService : IEcbService
{
    private readonly IGenericHttpClientFactory _clientFactory;

    public EcbService(IGenericHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<GesmesEnvelope> GetDailyRatesAsync()
    {
        var client = _clientFactory.CreateClient("ECBApi");
        return await client.GetXmlAsync<GesmesEnvelope>("/stats/eurofxref/eurofxref-daily.xml");
    }
}