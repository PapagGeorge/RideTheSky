using GenericHttpClient.Application.Configuration;
using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GenericHttpClient.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddGenericHttpClients(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<GenericHttpClientBuilder> configure)
    {
        var builder = new GenericHttpClientBuilder(services);
        configure(builder);
        
        services.Configure<EmailReputationConfig>(
            configuration.GetSection("EmailReputationConfig"));
        
        // Register the factory
        services.AddSingleton<IGenericHttpClientFactory, GenericHttpClientFactory>();
        services.AddScoped<IMainService, MainService>();
        services.AddScoped<IAbstractService, AbstractService>();
        services.AddScoped<IValidationService, ValidationService>();
        
        return services;
    }
}