using System.Text.Json.Serialization;

namespace GenericHttpClient.Domain.Models.ValidateVat;

public class VallidateVatResponse
{
    [JsonPropertyName("data")]
    public bool Data { get; set; }
}