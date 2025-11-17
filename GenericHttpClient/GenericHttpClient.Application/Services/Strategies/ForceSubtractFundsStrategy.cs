using GenericHttpClient.Application.Interfaces;

namespace GenericHttpClient.Application.Services.Strategies;

public class ForceSubtractFundsStrategy : IWalletBalanceStrategy
{
    public decimal Apply(decimal currentBalance, decimal amount)
        => currentBalance - amount;
}