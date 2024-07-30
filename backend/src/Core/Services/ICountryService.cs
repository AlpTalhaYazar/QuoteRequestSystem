using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Core.Services;

public interface ICountryService : IGenericService<Country>
{
    Task<IEnumerable<object>> GetCountriesWithCitiesAsync();
}