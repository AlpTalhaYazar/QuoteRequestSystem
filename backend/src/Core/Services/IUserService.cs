using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Core.Services;

public interface IUserService : IGenericService<User>
{
    Task<User> GetByEmailAsync(string email);

    Task<User> GetByEmailAndPasswordAsync(string email, string password);

    Task<(int Id, string Email, string Role)> AuthenticateAsync(string email, string password);

    Task<(int UserId, string FirstName, string LastName, string Email, string Role)> RegisterNewUserAsync(string email,
        string password, string firstName, string lastName);
}