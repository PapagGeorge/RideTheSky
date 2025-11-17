using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.Wallet;
using Microsoft.EntityFrameworkCore;

namespace GenericHttpClient.Infrastructure.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly ExchangeRateDbContext _db;

    public WalletRepository(ExchangeRateDbContext db)
    {
        _db = db;
    }

    public Task<Wallet?> GetByIdAsync(Guid id) =>
        _db.Wallets.FirstOrDefaultAsync(w => w.Id == id);

    public async Task AddAsync(Wallet wallet)
    {
        await _db.Wallets.AddAsync(wallet);
    }

    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}