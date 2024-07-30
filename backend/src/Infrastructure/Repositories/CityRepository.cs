using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class CityRepository(ApplicationDbContext context) : GenericRepository<City>(context), ICityRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<City> _dbSet = context.Set<City>();
}