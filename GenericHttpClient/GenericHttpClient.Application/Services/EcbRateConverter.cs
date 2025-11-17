using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.GetDailyRates;

namespace GenericHttpClient.Application.Services;

public class EcbRateConverter : IEcbRateConverter
{
    private readonly IEcbService _ecbService;

    public EcbRateConverter(IEcbService ecbService)
    {
        _ecbService = ecbService;
    }

    public async Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency)
    {
        if (amount < 0)
            throw new ArgumentException("Amount must be positive.");

        if (fromCurrency == toCurrency)
            return amount;

        var envelope = await _ecbService.GetDailyRatesAsync();

        var rateDict = ExtractRates(envelope);

        if (!rateDict.TryGetValue(fromCurrency, out decimal fromRate))
            throw new InvalidOperationException($"ECB does not provide rate for currency '{fromCurrency}'.");

        if (!rateDict.TryGetValue(toCurrency, out decimal toRate))
            throw new InvalidOperationException($"ECB does not provide rate for currency '{toCurrency}'.");

        // EUR → always 1.0
        // Convert: amount → EUR → target currency
        decimal amountInEur = amount / fromRate;
        decimal converted = amountInEur * toRate;

        return Math.Round(converted, 4);
    }

    private Dictionary<string, decimal> ExtractRates(GesmesEnvelope envelope)
    {
        var dict = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            ["EUR"] = 1.0m
        };

        var timeCubes = envelope.Cube?.TimeCubes;
        if (timeCubes == null || timeCubes.Count == 0)
            throw new InvalidOperationException("ECB response did not contain any currency rate data.");

        // Always take the first/latest time cube
        var latestCube = timeCubes.FirstOrDefault();
        if (latestCube?.Rates == null)
            throw new InvalidOperationException("ECB response contained a time cube with no currency rates.");

        foreach (var rate in latestCube.Rates)
        {
            dict[rate.Currency] = rate.Rate;
        }

        return dict;
    }
}