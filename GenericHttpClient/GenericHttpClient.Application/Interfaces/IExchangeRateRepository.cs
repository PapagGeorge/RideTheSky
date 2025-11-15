using GenericHttpClient.Domain.DBModels;

namespace GenericHttpClient.Application.Interfaces;

public interface IExchangeRateRepository
{
    Task MergeRatesAsync(IEnumerable<ExchangeRate> rates, CancellationToken cancellationToken = default);
}