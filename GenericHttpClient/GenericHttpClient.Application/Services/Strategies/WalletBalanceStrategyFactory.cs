using GenericHttpClient.Application.Interfaces;

namespace GenericHttpClient.Application.Services.Strategies;

public class WalletBalanceStrategyFactory : IWalletBalanceStrategyFactory
{
    public IWalletBalanceStrategy Create(string strategyName)
    {
        return strategyName.ToLower() switch
        {
            "addfunds" => new AddFundsStrategy(),
            "subtractfunds" => new SubtractFundsStrategy(),
            "forcesubtractfunds" => new ForceSubtractFundsStrategy(),
            _ => throw new ArgumentException($"Unknown strategy: {strategyName}")
        };
    }
}