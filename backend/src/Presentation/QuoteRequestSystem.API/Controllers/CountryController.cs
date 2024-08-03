using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuoteRequestSystem.API.Models;
using QuoteRequestSystem.Application.DTOs;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/country")]
public class CountryController(IServiceFactory serviceFactory) : ControllerBase
{
    private readonly ICountryService _countryService = serviceFactory.CreateCountryService();

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<Country>>> GetCountries()
    {
        var countries = await _countryService.GetAllAsync();

        return new ApiResponse<IEnumerable<Country>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = countries,
            Message = "Countries found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<Country>> GetCountryById(int id)
    {
        var country = await _countryService.GetByIdAsync(id);

        if (country is null)
        {
            return new ApiResponse<Country>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Country not found",
                Errors = ["Country not found"]
            };
        }

        return new ApiResponse<Country>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = country,
            Message = "Country found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost]
    public async Task<ApiResponse<Country>> AddCountry(Country country)
    {
        country.CreatedAt = DateTime.UtcNow;
        country.UpdatedAt = DateTime.UtcNow;

        await _countryService.AddAsync(country);

        return new ApiResponse<Country>
        {
            IsSuccess = true,
            StatusCode = 201,
            Data = country,
            Message = "Country created",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse<Country>> UpdateCountry(int id, Country country)
    {
        if (id != country.Id)
        {
            return new ApiResponse<Country>
            {
                IsSuccess = false,
                StatusCode = 400,
                Data = null,
                Message = "Country id mismatch",
                Errors = ["Country id mismatch"]
            };
        }

        country.UpdatedAt = DateTime.UtcNow;

        await _countryService.UpdateAsync(country);

        return new ApiResponse<Country>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = country,
            Message = "Country updated",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse<Country>> DeleteCountry(int id)
    {
        var country = await _countryService.GetByIdAsync(id);

        if (country is null)
        {
            return new ApiResponse<Country>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Country not found",
                Errors = ["Country not found"]
            };
        }

        country.UpdatedAt = DateTime.UtcNow;
        country.DeletedAt = DateTime.UtcNow;
        country.IsDeleted = true;

        await _countryService.UpdateAsync(country);

        return new ApiResponse<Country>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = country,
            Message = "Country deleted",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("hard-delete/{id}")]
    public async Task<ApiResponse<Country>> HardDeleteCountry(int id)
    {
        var country = await _countryService.GetByIdAsync(id);

        if (country is null)
        {
            return new ApiResponse<Country>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Country not found",
                Errors = ["Country not found"]
            };
        }

        await _countryService.DeleteAsync(id);

        return new ApiResponse<Country>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = country,
            Message = "Country hard deleted",
            Errors = Array.Empty<string>()
        };
    }

    [HttpGet("with-cities")]
    public async Task<ApiResponse<IEnumerable<CountryDto>>> GetCountriesWithCities()
    {
        var countriesWithCities = await _countryService.GetCountriesWithCitiesAsync();

        return new ApiResponse<IEnumerable<CountryDto>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = (IEnumerable<CountryDto>)countriesWithCities,
            Message = "Countries found",
            Errors = Array.Empty<string>()
        };
    }
}