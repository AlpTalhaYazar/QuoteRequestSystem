using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class CountryRepository(ApplicationDbContext context) : GenericRepository<Country>(context), ICountryRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<Country> _dbSet = context.Set<Country>();

    public async Task<IEnumerable<Country>> GetCountriesWithCitiesAsync()
    {
        return await _dbSet.Include(c => c.Cities).ToListAsync();
    }
}