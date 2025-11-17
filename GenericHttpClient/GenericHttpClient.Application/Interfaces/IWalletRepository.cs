using GenericHttpClient.Domain.Models.Wallet;

namespace GenericHttpClient.Application.Interfaces;

public interface IWalletRepository
{
    Task<Wallet?> GetByIdAsync(Guid id);
    Task AddAsync(Wallet wallet);
    Task SaveChangesAsync();
}