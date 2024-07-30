using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Core.UnitOfWork;
using QuoteRequestSystem.Infrastructure.Data;
using QuoteRequestSystem.Infrastructure.Repositories;

namespace QuoteRequestSystem.Infrastructure.UnitOfWork;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork, IAsyncDisposable
{
    public ICityRepository CityRepository => new CityRepository(context);
    public ICountryRepository CountryRepository => new CountryRepository(context);
    public IDimensionRepository DimensionRepository => new DimensionRepository(context);
    public IOfferRepository OfferRepository => new OfferRepository(context);
    public IQuoteRepository QuoteRepository => new QuoteRepository(context);
    public IUserRepository UserRepository => new UserRepository(context);
    public IUserRoleRepository UserRoleRepository => new UserRoleRepository(context);

    public IGenericRepository<T> GetRepository<T>() where T : class
    {
        return new GenericRepository<T>(context);
    }

    public Task<int> SaveChangesAsync()
    {
        return context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        await context.DisposeAsync();
    }
}