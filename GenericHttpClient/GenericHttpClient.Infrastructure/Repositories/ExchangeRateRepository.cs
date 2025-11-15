using System.Text;
using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.DBModels;
using Microsoft.EntityFrameworkCore;

namespace GenericHttpClient.Infrastructure.Repositories;

public class ExchangeRateMergeRepository : IExchangeRateRepository
{
    private readonly ExchangeRateDbContext _dbContext;

    public ExchangeRateMergeRepository(ExchangeRateDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task MergeRatesAsync(IEnumerable<ExchangeRate> rates, CancellationToken cancellationToken = default)
    {
        // Convert the rates to a VALUES block for SQL MERGE
        var sb = new StringBuilder();

        foreach (var r in rates)
        {
            sb.AppendLine(
                $"({r.Date.ToString("yyyy-MM-dd")}, '{r.CurrencyCode}', {r.Rate}, GETUTCDATE()),"
            );
        }

        // Remove last comma
        var valuesSql = sb.ToString().TrimEnd(',', '\n', '\r');

        var sql = $@"
MERGE INTO ExchangeRates AS Target
USING (VALUES 
    {valuesSql}
) AS Source ([Date], [CurrencyCode], [Rate], [UpdatedAt])
ON Target.[Date] = Source.[Date] AND Target.[CurrencyCode] = Source.[CurrencyCode]
WHEN MATCHED THEN 
    UPDATE SET 
        Target.[Rate] = Source.[Rate],
        Target.[UpdatedAt] = GETUTCDATE()
WHEN NOT MATCHED THEN
    INSERT ([Date], [CurrencyCode], [Rate], [UpdatedAt])
    VALUES (Source.[Date], Source.[CurrencyCode], Source.[Rate], GETUTCDATE());
";

        await _dbContext.Database.ExecuteSqlRawAsync(sql, cancellationToken);
    }
}