using System.Text.Json.Serialization;

namespace GenericHttpClient.Domain.Models.GetEmailReputation
{
    public class GetEmailReputationResponse
    {
        [JsonPropertyName("email_address")]
        public string EmailAddress { get; set; } = string.Empty;

        [JsonPropertyName("email_deliverability")]
        public EmailDeliverability? EmailDeliverability { get; set; }

        [JsonPropertyName("email_quality")]
        public EmailQuality? EmailQuality { get; set; }

        [JsonPropertyName("email_sender")]
        public EmailSender? EmailSender { get; set; }

        [JsonPropertyName("email_domain")]
        public EmailDomain? EmailDomain { get; set; }

        [JsonPropertyName("email_risk")]
        public EmailRisk? EmailRisk { get; set; }

        [JsonPropertyName("email_breaches")]
        public EmailBreaches? EmailBreaches { get; set; }
    }

    public class EmailDeliverability
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("status_detail")]
        public string StatusDetail { get; set; } = string.Empty;

        [JsonPropertyName("is_format_valid")]
        public bool? IsFormatValid { get; set; }

        [JsonPropertyName("is_smtp_valid")]
        public bool? IsSmtpValid { get; set; }

        [JsonPropertyName("is_mx_valid")]
        public bool? IsMxValid { get; set; }

        [JsonPropertyName("mx_records")]
        public List<string>? MxRecords { get; set; }
    }

    public class EmailQuality
    {
        [JsonPropertyName("score")]
        public string Score { get; set; } = string.Empty;

        [JsonPropertyName("is_free_email")]
        public bool? IsFreeEmail { get; set; }

        [JsonPropertyName("is_username_suspicious")]
        public bool? IsUsernameSuspicious { get; set; }

        [JsonPropertyName("is_disposable")]
        public bool? IsDisposable { get; set; }

        [JsonPropertyName("is_catchall")]
        public bool? IsCatchall { get; set; }

        [JsonPropertyName("is_subaddress")]
        public bool? IsSubaddress { get; set; }

        [JsonPropertyName("is_role")]
        public bool? IsRole { get; set; }

        [JsonPropertyName("is_dmarc_enforced")]
        public bool? IsDmarcEnforced { get; set; }

        [JsonPropertyName("is_spf_strict")]
        public bool? IsSpfStrict { get; set; }

        [JsonPropertyName("minimum_age")]
        public int? MinimumAge { get; set; }
    }

    public class EmailSender
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("email_provider_name")]
        public string EmailProviderName { get; set; } = string.Empty;

        [JsonPropertyName("organization_name")]
        public string OrganizationName { get; set; } = string.Empty;

        [JsonPropertyName("organization_type")]
        public string OrganizationType { get; set; } = string.Empty;
    }

    public class EmailDomain
    {
        [JsonPropertyName("domain")]
        public string Domain { get; set; } = string.Empty;

        [JsonPropertyName("domain_age")]
        public int? DomainAge { get; set; }

        [JsonPropertyName("is_live_site")]
        public bool? IsLiveSite { get; set; }

        [JsonPropertyName("registrar")]
        public string Registrar { get; set; } = string.Empty;

        [JsonPropertyName("registrar_url")]
        public string RegistrarUrl { get; set; } = string.Empty;

        [JsonPropertyName("date_registered")]
        public string DateRegistered { get; set; } = string.Empty;

        [JsonPropertyName("date_last_renewed")]
        public string DateLastRenewed { get; set; } = string.Empty;

        [JsonPropertyName("date_expires")]
        public string DateExpires { get; set; } = string.Empty;

        [JsonPropertyName("is_risky_tld")]
        public bool? IsRiskyTld { get; set; }
    }

    public class EmailRisk
    {
        [JsonPropertyName("address_risk_status")]
        public string AddressRiskStatus { get; set; } = string.Empty;

        [JsonPropertyName("domain_risk_status")]
        public string DomainRiskStatus { get; set; } = string.Empty;
    }

    public class EmailBreaches
    {
        [JsonPropertyName("total_breaches")]
        public int? TotalBreaches { get; set; }

        [JsonPropertyName("date_first_breached")]
        public string DateFirstBreached { get; set; } = string.Empty;

        [JsonPropertyName("date_last_breached")]
        public string DateLastBreached { get; set; } = string.Empty;

        [JsonPropertyName("breached_domains")]
        public List<BreachedDomain>? BreachedDomains { get; set; }
    }

    public class BreachedDomain
    {
        [JsonPropertyName("domain")]
        public string Domain { get; set; } = string.Empty;

        [JsonPropertyName("breach_date")]
        public string DateBreached { get; set; } = string.Empty;
    }
}
