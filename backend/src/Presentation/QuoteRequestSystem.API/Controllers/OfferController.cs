using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuoteRequestSystem.API.Models;
using QuoteRequestSystem.Application.DTOs;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/offer")]
public class OfferController(IServiceFactory serviceFactory) : ControllerBase
{
    private readonly IOfferService _offerService = serviceFactory.CreateOfferService();

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<OfferResponseDto>>> GetOffers()
    {
        var offers = await _offerService.GetAllAsync();

        if (offers?.Count() is null or 0)
        {
            return new ApiResponse<IEnumerable<OfferResponseDto>>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Offers not found",
                Errors = ["Offers not found"]
            };
        }

        var offersDto = offers.Select(x => new OfferResponseDto()
        {
            Id = x.Id,
            QuoteId = x.QuoteId,
            OfferCurrencyType = offers.FirstOrDefault(y => y.QuoteId == x.QuoteId)?.CurrencyUnit,
            OfferAmount = offers.FirstOrDefault(y => y.QuoteId == x.QuoteId)?.Price ?? 0,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        });

        return new ApiResponse<IEnumerable<OfferResponseDto>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = offersDto,
            Message = "Offers found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<Offer>> GetOfferById(int id)
    {
        var offer = await _offerService.GetByIdAsync(id);

        if (offer is null)
        {
            return new ApiResponse<Offer>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Offer not found",
                Errors = ["Offer not found"]
            };
        }

        return new ApiResponse<Offer>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = offer,
            Message = "Offer found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost]
    public async Task<ApiResponse<Offer>> AddOffer(Offer offer)
    {
        offer.CreatedAt = DateTime.UtcNow;

        var addedOffer = await _offerService.AddAsync(offer);

        return new ApiResponse<Offer>
        {
            IsSuccess = true,
            StatusCode = 201,
            Data = addedOffer,
            Message = "Offer added",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse<Offer>> UpdateOffer(int id, Offer offer)
    {
        if (id != offer.Id)
        {
            return new ApiResponse<Offer>
            {
                IsSuccess = false,
                StatusCode = 400,
                Data = null,
                Message = "Offer ID mismatch",
                Errors = ["Offer ID mismatch"]
            };
        }

        offer.UpdatedAt = DateTime.UtcNow;

        await _offerService.UpdateAsync(offer);

        return new ApiResponse<Offer>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = offer,
            Message = "Offer updated",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse<Offer>> DeleteOffer(int id)
    {
        var offer = await _offerService.GetByIdAsync(id);

        if (offer is null)
        {
            return new ApiResponse<Offer>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Offer not found",
                Errors = ["Offer not found"]
            };
        }

        offer.UpdatedAt = DateTime.UtcNow;
        offer.DeletedAt = DateTime.UtcNow;
        offer.IsDeleted = true;

        await _offerService.UpdateAsync(offer);

        return new ApiResponse<Offer>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = offer,
            Message = "Offer deleted",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("hard/{id}")]
    public async Task<ApiResponse<Offer>> HardDeleteOffer(int id)
    {
        var offer = await _offerService.GetByIdAsync(id);

        if (offer is null)
        {
            return new ApiResponse<Offer>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Offer not found",
                Errors = ["Offer not found"]
            };
        }

        await _offerService.DeleteAsync(id);

        return new ApiResponse<Offer>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = offer,
            Message = "Offer hard deleted",
            Errors = Array.Empty<string>()
        };
    }
}