using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuoteRequestSystem.API.Models;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/dımensıon")]
public class DimensionController(IServiceFactory serviceFactory) : ControllerBase
{
    private readonly IDimensionService _dimensionService = serviceFactory.CreateDimensionService();

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<Dimension>>> GetDimensions()
    {
        var dimensions = await _dimensionService.GetAllAsync();

        return new ApiResponse<IEnumerable<Dimension>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = dimensions,
            Message = "Dimensions found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<Dimension>> GetDimensionById(int id)
    {
        var dimension = await _dimensionService.GetByIdAsync(id);

        if (dimension is null)
        {
            return new ApiResponse<Dimension>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Dimension not found",
                Errors = ["Dimension not found"]
            };
        }

        return new ApiResponse<Dimension>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = dimension,
            Message = "Dimension found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost]
    public async Task<ApiResponse<Dimension>> AddDimension(Dimension dimension)
    {
        dimension.CreatedAt = DateTime.UtcNow;
        dimension.UpdatedAt = DateTime.UtcNow;

        await _dimensionService.AddAsync(dimension);

        return new ApiResponse<Dimension>
        {
            IsSuccess = true,
            StatusCode = 201,
            Data = dimension,
            Message = "Dimension created",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPut]
    public async Task<ApiResponse<Dimension>> UpdateDimension(Dimension dimension)
    {
        if (dimension.Id == 0)
        {
            return new ApiResponse<Dimension>
            {
                IsSuccess = false,
                StatusCode = 400,
                Data = null,
                Message = "Dimension not found",
                Errors = ["Dimension not found"]
            };
        }

        dimension.UpdatedAt = DateTime.UtcNow;

        await _dimensionService.UpdateAsync(dimension);

        return new ApiResponse<Dimension>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = dimension,
            Message = "Dimension updated",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse<Dimension>> DeleteDimension(int id)
    {
        var dimension = await _dimensionService.GetByIdAsync(id);

        if (dimension is null)
        {
            return new ApiResponse<Dimension>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Dimension not found",
                Errors = ["Dimension not found"]
            };
        }

        dimension.UpdatedAt = DateTime.UtcNow;
        dimension.DeletedAt = DateTime.UtcNow;
        dimension.IsDeleted = true;

        await _dimensionService.DeleteAsync(id);

        return new ApiResponse<Dimension>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = dimension,
            Message = "Dimension deleted",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("hard/{id}")]
    public async Task<ApiResponse<Dimension>> HardDeleteDimension(int id)
    {
        var dimension = await _dimensionService.GetByIdAsync(id);

        if (dimension is null)
        {
            return new ApiResponse<Dimension>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Dimension not found",
                Errors = ["Dimension not found"]
            };
        }

        await _dimensionService.DeleteAsync(id);

        return new ApiResponse<Dimension>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = dimension,
            Message = "Dimension deleted",
            Errors = Array.Empty<string>()
        };
    }
}