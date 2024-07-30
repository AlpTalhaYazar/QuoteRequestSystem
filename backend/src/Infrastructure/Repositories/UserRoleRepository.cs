using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class UserRoleRepository(ApplicationDbContext context)
    : GenericRepository<UserRole>(context), IUserRoleRepository
{
}