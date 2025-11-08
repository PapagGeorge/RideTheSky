using GenericHttpClient.Application.Interfaces;
using GenericHttpClient.Domain;

namespace GenericHttpClient.Application;

public class UserService
{
    private readonly GenericHttpClient _client;

    public UserService(IGenericHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient("UsersApi");
    }

    public async Task<Model.User?> GetUserAsync(int userId)
    {
        return await _client.GetAsync<Model.User>($"/users/{userId}");
    }

    public async Task<Model.User?> CreateUserAsync(Model.CreateUserRequest request)
    {
        return await _client.PostAsync<Model.CreateUserRequest, Model.User>("/users", request);
    }
}