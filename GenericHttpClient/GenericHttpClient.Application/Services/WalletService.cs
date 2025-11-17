using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain.Models.Wallet;

namespace GenericHttpClient.Application.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _walletRepository;
    private readonly IWalletBalanceStrategyFactory _strategyFactory;
    private readonly IEcbRateConverter _rateConverter; // for FX conversion

    public WalletService(
        IWalletRepository walletRepository,
        IWalletBalanceStrategyFactory strategyFactory,
        IEcbRateConverter rateConverter)
    {
        _walletRepository = walletRepository;
        _strategyFactory = strategyFactory;
        _rateConverter = rateConverter;
    }

    public async Task<Guid> CreateWalletAsync(string currency)
    {
        var wallet = new Wallet
        {
            Balance = 0,
            Currency = currency
        };

        await _walletRepository.AddAsync(wallet);
        await _walletRepository.SaveChangesAsync();

        return wallet.Id;
    }

    public async Task<decimal> GetBalanceAsync(Guid id, string? convertToCurrency = null)
    {
        var wallet = await _walletRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Wallet not found");

        if (convertToCurrency == null || convertToCurrency == wallet.Currency)
            return wallet.Balance;

        return await _rateConverter.ConvertAsync(
            wallet.Balance, wallet.Currency,  convertToCurrency);
    } 

    public async Task AdjustBalanceAsync(
        Guid id,
        decimal amount,
        string amountCurrency,
        string strategyName)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");

        var wallet = await _walletRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Wallet not found");

        IWalletBalanceStrategy strategy = _strategyFactory.Create(strategyName);

        decimal convertedAmount =
            await _rateConverter.ConvertAsync(amount, amountCurrency, wallet.Currency);

        wallet.Balance = strategy.Apply(wallet.Balance, convertedAmount);

        await _walletRepository.SaveChangesAsync();
    }
}