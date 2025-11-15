namespace GenericHttpClient.Application.Interfaces;

public interface IGenericHttpClientFactory
{
    HttpClientsInfrastructure.GenericHttpClient CreateClient(string name);
}