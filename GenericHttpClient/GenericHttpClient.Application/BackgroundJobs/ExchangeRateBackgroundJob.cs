using System.Text.Json;
using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.DBModels;
using GenericHttpClient.Domain.Models.GetDailyRates;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenericHttpClient.Application.BackgroundJobs;

public class ExchangeRateBackgroundJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ExchangeRateBackgroundJob> _logger;
    private const string CacheKey = "latest_exchange_rates";

    public ExchangeRateBackgroundJob(IServiceProvider serviceProvider, ILogger<ExchangeRateBackgroundJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Exchange rate background job started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var ecbService = scope.ServiceProvider.GetRequiredService<IEcbService>();
                var mergeRepo = scope.ServiceProvider.GetRequiredService<IExchangeRateRepository>();
                var cache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();

                await RunJobAsync(ecbService, mergeRepo, cache, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while running exchange rate job.");
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }

    private async Task RunJobAsync(
        IEcbService ecbService,
        IExchangeRateRepository mergeRepo,
        IDistributedCache cache,
        CancellationToken ct)
    {
        _logger.LogInformation("Fetching ECB daily rates...");

        var xml = await ecbService.GetDailyRatesAsync();
        var rates = ParseRates(xml);

        await mergeRepo.MergeRatesAsync(rates, ct);

        await UpdateCacheAsync(rates, cache, ct);

        _logger.LogInformation("Exchange rates updated and cached.");
    }

    private static List<ExchangeRate> ParseRates(GesmesEnvelope xml)
    {
        var timeCube = xml.Cube.TimeCubes.First(); // Daily rates → 1 element
        var date = DateOnly.Parse(timeCube.Time);

        return timeCube.Rates.Select(r =>
            new ExchangeRate
            {
                Date = date,
                CurrencyCode = r.Currency,
                Rate = r.Rate,
                UpdatedAt = DateTime.UtcNow
            }).ToList();
    }

    private static async Task UpdateCacheAsync(IEnumerable<ExchangeRate> rates, IDistributedCache cache, CancellationToken ct)
    {
        var json = JsonSerializer.Serialize(rates);
        await cache.SetStringAsync(
            CacheKey,
            json,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            },
            ct);
    }

    public async Task<List<ExchangeRate>> GetLatestRatesAsync(IServiceProvider sp, CancellationToken ct = default)
    {
        try
        {
            using var scope = sp.CreateScope();
            var cache = scope.ServiceProvider.GetRequiredService<IDistributedCache>();
            var json = await cache.GetStringAsync(CacheKey, ct);

            if (!string.IsNullOrEmpty(json))
                return JsonSerializer.Deserialize<List<ExchangeRate>>(json) ?? new List<ExchangeRate>();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to read exchange rates from cache.");
        }

        return new List<ExchangeRate>();
    }
}