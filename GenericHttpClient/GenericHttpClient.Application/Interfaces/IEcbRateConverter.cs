namespace GenericHttpClient.Application.Interfaces;

public interface IEcbRateConverter
{
    Task<decimal> ConvertAsync(decimal amount, string fromCurrency, string toCurrency);
}
