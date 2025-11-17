namespace GenericHttpClient.Application.Interfaces;

public interface IWalletBalanceStrategy
{
    decimal Apply(decimal currentBalance, decimal amount);
}