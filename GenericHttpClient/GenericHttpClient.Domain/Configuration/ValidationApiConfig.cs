namespace GenericHttpClient.Application.Configuration;

public class ValidationApiConfig
{
    public VatValidation VatValidation { get; set; }
}

public class VatValidation
{
    public string ApiKey { get; set; } =  string.Empty;
}