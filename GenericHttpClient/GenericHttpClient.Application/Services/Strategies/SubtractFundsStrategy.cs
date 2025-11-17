using GenericHttpClient.Application.Interfaces;

namespace GenericHttpClient.Application.Services.Strategies;

public class SubtractFundsStrategy : IWalletBalanceStrategy
{
    public decimal Apply(decimal currentBalance, decimal amount)
    {
        if (currentBalance < amount)
            throw new InvalidOperationException("Insufficient funds.");

        return currentBalance - amount;
    }
}