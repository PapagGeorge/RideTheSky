
using GenericHttpClient.Domain.Models.GetEmailReputation;

namespace GenericHttpClient.Application;

public static class Mappings
{
    public static GetEmailReputationResponseDto ToMainResponse(this GetEmailReputationResponse source)
    {
        return new GetEmailReputationResponseDto
        {
            EmailAddress = source.EmailAddress,
            DeliverabilityStatus = source.EmailDeliverability?.Status ?? string.Empty,
            QualityScore = source.EmailQuality?.Score,
            ProviderName = source.EmailSender?.EmailProviderName ?? string.Empty,
            Organization = source.EmailSender?.OrganizationName ?? string.Empty,
            RiskLevel = source.EmailRisk?.AddressRiskStatus ?? string.Empty,
            TotalBreaches = source.EmailBreaches?.TotalBreaches,
            LastBreached = source.EmailBreaches?.DateLastBreached.ToNullableDateTime(),
            IsFreeEmail = source.EmailQuality?.IsFreeEmail,
            IsDisposable = source.EmailQuality?.IsDisposable,
            IsCatchall = source.EmailQuality?.IsCatchall
        };
    }
}