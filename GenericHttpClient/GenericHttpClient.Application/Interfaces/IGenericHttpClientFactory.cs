namespace GenericHttpClient.Application.Interfaces;

public interface IGenericHttpClientFactory
{
    GenericHttpClient CreateClient(string name);
}