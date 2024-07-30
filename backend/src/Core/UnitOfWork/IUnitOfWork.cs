using QuoteRequestSystem.Core.Repositories;

namespace QuoteRequestSystem.Core.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICityRepository CityRepository { get; }
    ICountryRepository CountryRepository { get; }
    IDimensionRepository DimensionRepository { get; }
    IOfferRepository OfferRepository { get; }
    IQuoteRepository QuoteRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }

    IGenericRepository<T> GetRepository<T>() where T : class;

    Task<int> SaveChangesAsync();
}