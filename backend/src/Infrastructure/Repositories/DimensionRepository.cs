using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class DimensionRepository(ApplicationDbContext context)
    : GenericRepository<Dimension>(context), IDimensionRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<Dimension> _dbSet = context.Set<Dimension>();
}