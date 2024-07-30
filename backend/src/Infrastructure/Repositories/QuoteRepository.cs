using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class QuoteRepository(ApplicationDbContext context) : GenericRepository<Quote>(context), IQuoteRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<Quote> _dbSet = context.Set<Quote>();
}