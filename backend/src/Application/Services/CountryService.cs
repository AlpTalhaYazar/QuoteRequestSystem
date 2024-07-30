using QuoteRequestSystem.Application.DTOs;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Application.Services;

public class CountryService : GenericService<Country>, ICountryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CountryService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<object>> GetCountriesWithCitiesAsync()
    {
        IEnumerable<Country> countriesWithCities = await _unitOfWork.CountryRepository.GetCountriesWithCitiesAsync();

        IEnumerable<CountryDto> countriesWithCitiesDto = countriesWithCities.Select(c => new CountryDto
        {
            Id = c.Id,
            Name = c.Name,
            Cities = c.Cities.Select(city => new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                CreatedAt = city.CreatedAt,
                UpdatedAt = city.UpdatedAt
            }).ToList(),
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });

        return countriesWithCitiesDto;
    }
}