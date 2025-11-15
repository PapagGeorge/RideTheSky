using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Infrastructure.Configuration;
using GenericHttpClient.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GenericHttpClient.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.AddDbContexts();
        services.AddScoped<IExchangeRateRepository, ExchangeRateMergeRepository>();
        return services;
    }
    
    private static void AddDbContexts(this IServiceCollection services)
    {
        services.AddDbContext<ExchangeRateDbContext>((serviceProvider, options) =>
        {
            var dbOptions = serviceProvider.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.UseSqlServer(dbOptions.ExchangeRateDb, sql =>
            {
                sql.MigrationsAssembly(typeof(ExchangeRateDbContext).Assembly.FullName);
            });
        });
    }
}