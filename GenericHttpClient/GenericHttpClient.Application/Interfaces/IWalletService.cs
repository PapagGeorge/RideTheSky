namespace GenericHttpClient.Application.Interfaces;

public interface IWalletService
{
    Task<Guid> CreateWalletAsync(string currency);
    Task<decimal> GetBalanceAsync(Guid id, string convertToCurrency = null);
    Task AdjustBalanceAsync(Guid id, decimal amount, string amountCurrency, string strategyName);
}