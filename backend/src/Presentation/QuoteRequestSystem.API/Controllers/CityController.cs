using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuoteRequestSystem.API.Models;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/city")]
public class CityController(IServiceFactory serviceFactory) : ControllerBase
{
    private readonly ICityService cityService = serviceFactory.CreateCityService();

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<City>>> GetCities()
    {
        var cities = await cityService.GetAllAsync();

        return new ApiResponse<IEnumerable<City>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = cities,
            Message = "Cities found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<City>> GetCityById(int id)
    {
        var city = await cityService.GetByIdAsync(id);

        if (city is null)
        {
            return new ApiResponse<City>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "City not found",
                Errors = ["City not found"]
            };
        }

        return new ApiResponse<City>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = city,
            Message = "City found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost]
    public async Task<ApiResponse<City>> AddCity(City city)
    {
        city.CreatedAt = DateTime.UtcNow;
        city.UpdatedAt = DateTime.UtcNow;

        await cityService.AddAsync(city);

        return new ApiResponse<City>
        {
            IsSuccess = true,
            StatusCode = 201,
            Data = city,
            Message = "City created",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse<City>> UpdateCity(int id, City city)
    {
        if (id != city.Id)
        {
            return new ApiResponse<City>
            {
                IsSuccess = false,
                StatusCode = 400,
                Data = null,
                Message = "Id mismatch",
                Errors = ["Id mismatch"]
            };
        }

        city.UpdatedAt = DateTime.UtcNow;

        await cityService.UpdateAsync(city);

        return new ApiResponse<City>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = city,
            Message = "City updated",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse<bool>> DeleteCity(int id)
    {
        var city = await cityService.GetByIdAsync(id);

        if (city is null)
        {
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = false,
                Message = "City not found",
                Errors = ["City not found"]
            };
        }

        city.UpdatedAt = DateTime.UtcNow;
        city.DeletedAt = DateTime.UtcNow;
        city.IsDeleted = true;

        await cityService.UpdateAsync(city);

        return new ApiResponse<bool>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = true,
            Message = "City deleted",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("hard/{id}")]
    public async Task<ApiResponse<bool>> HardDeleteCity(int id)
    {
        var city = await cityService.GetByIdAsync(id);

        if (city is null)
        {
            return new ApiResponse<bool>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = false,
                Message = "City not found",
                Errors = ["City not found"]
            };
        }

        await cityService.DeleteAsync(id);

        return new ApiResponse<bool>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = true,
            Message = "City hard deleted",
            Errors = Array.Empty<string>()
        };
    }
}