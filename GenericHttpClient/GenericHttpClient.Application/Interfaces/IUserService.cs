using GenericHttpClient.Domain;

namespace GenericHttpClient.Application.Interfaces;

public interface IUserService
{
    Task<Model.User?> GetUserAsync(int userId);
    Task<Model.User?> CreateUserAsync(Model.CreateUserRequest request);
}