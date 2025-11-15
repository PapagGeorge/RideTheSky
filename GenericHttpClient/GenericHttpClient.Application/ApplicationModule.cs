using GenericHttpClient.Application.BackgroundJobs;
using GenericHttpClient.Application.Configuration;
using GenericHttpClient.Application.HttpClientsInfrastructure;
using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GenericHttpClient.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<GenericHttpClientBuilder> configure)
    {
        var builder = new GenericHttpClientBuilder(services);
        configure(builder);
        
        services.AddHostedService<ExchangeRateBackgroundJob>();
        services.AddSingleton<IGenericHttpClientFactory, GenericHttpClientFactory>();
        services.AddScoped<IMainService, MainService>();
        services.AddScoped<IAbstractService, AbstractService>();
        services.AddScoped<IValidationService, ValidationService>();
        // Register the raw HTTP service
        services.AddScoped<EcbService>();

        // Register IEcbService as the cached decorator
        services.AddScoped<IEcbService>(sp =>
        {
            var inner = sp.GetRequiredService<EcbService>();
            var cache = sp.GetRequiredService<IDistributedCache>();
            var logger = sp.GetRequiredService<ILogger<CachedEcbService>>();
            return new CachedEcbService(inner, cache, logger);
        });
        
        services.AddStackExchangeRedisCache(options =>
        {
            var redisOptions = configuration.GetSection("Redis").Get<RedisOptions>();
            options.Configuration = redisOptions.ConnectionString;
            options.InstanceName = redisOptions.InstanceName;
        });
        
        return services;
    }
}