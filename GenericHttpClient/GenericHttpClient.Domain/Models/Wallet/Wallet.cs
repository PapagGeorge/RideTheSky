namespace GenericHttpClient.Domain.Models.Wallet;

public class Wallet
{
    public Guid Id { get; set; }
    public decimal Balance { get; set; }
    public string Currency { get; set; }
}