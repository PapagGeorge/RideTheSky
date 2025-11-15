using GenericHttpClient.Domain.Models.GetDailyRates;

namespace GenericHttpClient.Application.Interfaces;

public interface IEcbService
{
    Task<GesmesEnvelope> GetDailyRatesAsync();
}