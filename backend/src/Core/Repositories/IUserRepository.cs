using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Core.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);

    Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash);
}