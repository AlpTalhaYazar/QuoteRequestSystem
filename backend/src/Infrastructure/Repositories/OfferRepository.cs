using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Domain.Entities;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class OfferRepository(ApplicationDbContext context) : GenericRepository<Offer>(context), IOfferRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly DbSet<Offer> _dbSet = context.Set<Offer>();
}