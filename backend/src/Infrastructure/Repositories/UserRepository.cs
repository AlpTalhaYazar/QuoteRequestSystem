using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<User> _dbSet = context.Set<User>();

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _dbSet.Include(x => x.Role).FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> GetByEmailAndPasswordAsync(string email, string passwordHash)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == passwordHash);
    }
}