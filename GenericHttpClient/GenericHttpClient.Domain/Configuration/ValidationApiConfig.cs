namespace GenericHttpClient.Application.Configuration;

public class ValidationApiConfig
{
    public VatValidation VatValidation { get; set; }
}

public class VatValidation
{
    public string ApiKey { get; set; } =  string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string BaseAddress { get; set; } = string.Empty;
    public Dictionary<string, string> Headers { get; set; } = new();
}