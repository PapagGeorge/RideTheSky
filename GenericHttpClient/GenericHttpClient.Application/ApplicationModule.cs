using GenericHttpClient.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GenericHttpClient.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddGenericHttpClients(
        this IServiceCollection services,
        Action<GenericHttpClientBuilder> configure)
    {
        var builder = new GenericHttpClientBuilder(services);
        configure(builder);
        
        // Register the factory
        services.AddSingleton<IGenericHttpClientFactory, GenericHttpClientFactory>();
        
        return services;
    }
}