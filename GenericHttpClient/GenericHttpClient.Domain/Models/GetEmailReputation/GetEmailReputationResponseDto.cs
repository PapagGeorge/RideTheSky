namespace GenericHttpClient.Domain.Models.GetEmailReputation;

public class GetEmailReputationResponseDto
{
    public string EmailAddress { get; set; } = string.Empty;
    public string DeliverabilityStatus { get; set; } = string.Empty;
    public string QualityScore { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
    public string Organization { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = string.Empty;
    public int? TotalBreaches { get; set; }
    public DateTime? LastBreached { get; set; }
    public bool? IsFreeEmail { get; set; }
    public bool? IsDisposable { get; set; }
    public bool? IsCatchall { get; set; }
}