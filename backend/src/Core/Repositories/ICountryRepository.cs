using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Core.Repositories;

public interface ICountryRepository : IGenericRepository<Country>
{
    Task<IEnumerable<Country>> GetCountriesWithCitiesAsync();
}