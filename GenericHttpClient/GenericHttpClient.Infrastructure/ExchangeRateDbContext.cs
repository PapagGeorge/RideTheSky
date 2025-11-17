using GenericHttpClient.Domain.DBModels;
using GenericHttpClient.Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;

namespace GenericHttpClient.Infrastructure;

public class ExchangeRateDbContext : DbContext
{
    public DbSet<ExchangeRate> ExchangeRates { get; set; }
    public DbSet<Wallet> Wallets => Set<Wallet>();

    public ExchangeRateDbContext(DbContextOptions<ExchangeRateDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ExchangeRate>()
            .HasIndex(x => new { x.Date, x.CurrencyCode })
            .IsUnique();
    }
}