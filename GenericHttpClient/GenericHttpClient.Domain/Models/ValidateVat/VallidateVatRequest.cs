using System.Text.Json.Serialization;

namespace GenericHttpClient.Domain.Models.ValidateVat;

public class VallidateVatRequest
{
    [JsonPropertyName("vat")]
    public string Vat { get; set; } = string.Empty;
}