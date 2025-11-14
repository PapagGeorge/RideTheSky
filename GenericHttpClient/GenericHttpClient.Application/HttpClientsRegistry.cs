using GenericHttpClient.Application.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GenericHttpClient.Application;

public static class HttpClientsRegistry
{
    public static HttpClientRegistration EmailReputation(IOptions<EmailReputationConfig> options)
    {
        var config = options.Value;

        return new HttpClientRegistration
        {
            ClientName = config.ClientName,
            BaseAddress = config.BaseAddress,
        };
    }

    public static HttpClientRegistration ValidationApi(IOptions<ValidationApiConfig> options)
    {
        var config = options.Value.VatValidation;

        return new HttpClientRegistration
        {
            ClientName = config.ClientName,
            BaseAddress = config.BaseAddress,
            Configure = builder =>
            {
                builder.ConfigureHttpClient(c =>
                {
                    foreach (var header in config.Headers)
                        c.DefaultRequestHeaders.Add(header.Key, header.Value);
                });
            }
        };
    }
}

