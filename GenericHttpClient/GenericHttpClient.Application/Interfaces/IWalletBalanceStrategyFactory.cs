namespace GenericHttpClient.Application.Interfaces;

public interface IWalletBalanceStrategyFactory
{
    IWalletBalanceStrategy Create(string strategyName);
}