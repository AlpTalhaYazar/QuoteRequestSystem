using System.Security.Claims;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using QuoteRequestSystem.API.Models;
using QuoteRequestSystem.Application.DTOs;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.API.Controllers;

[ApiController]
[ApiVersion(1)]
[Route("api/v{v:apiVersion}/quote")]
public class QuoteController(IServiceFactory serviceFactory) : ControllerBase
{
    private readonly IQuoteService _quoteService = serviceFactory.CreateQuoteService();

    [HttpGet]
    public async Task<ApiResponse<IEnumerable<Quote>>> GetQuotes()
    {
        var quotes = await _quoteService.GetAllAsync();

        return new ApiResponse<IEnumerable<Quote>>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = quotes,
            Message = "Quotes found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpGet("{id}")]
    public async Task<ApiResponse<Quote>> GetQuoteById(int id)
    {
        var quote = await _quoteService.GetByIdAsync(id);

        if (quote is null)
        {
            return new ApiResponse<Quote>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Quote not found",
                Errors = new[] { "Quote not found" }
            };
        }

        return new ApiResponse<Quote>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = quote,
            Message = "Quote found",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPost]
    public async Task<ApiResponse<QuoteDto>> AddQuote(QuoteCreateDto quote)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        Console.WriteLine(userId);

        Quote newQuote = new()
        {
            UserId = int.Parse(userId),
            Mode = quote.Mode,
            MovementType = quote.MovementType,
            Incoterms = quote.Incoterms,
            CountryId = quote.CountryId,
            CityId = quote.CityId,
            PackageType = quote.PackageType,
            PackageDimensionUnit = quote.PackageDimensionUnit,
            PackageAmount = quote.PackageAmount,
            WeightUnit = quote.WeightUnit,
            WeightValue = quote.WeightValue,
            CurrencyUnit = quote.Currency,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _quoteService.AddAsync(newQuote);

        Offer offer = new();
        offer.QuoteId = newQuote.Id;
        offer.Price = await CalculatePriceForOfferFromQuote(newQuote);
        offer.CurrencyUnit = quote.Currency;
        offer.CreatedAt = DateTime.UtcNow;
        offer.UpdatedAt = DateTime.UtcNow;

        await serviceFactory.CreateOfferService().AddAsync(offer);

        QuoteDto newQuoteDto = new()
        {
            UserId = newQuote.UserId,
            Mode = newQuote.Mode,
            MovementType = newQuote.MovementType,
            Incoterms = newQuote.Incoterms,
            CountryId = newQuote.CountryId,
            CityId = newQuote.CityId,
            PackageType = newQuote.PackageType,
            PackageDimensionUnit = newQuote.PackageDimensionUnit,
            PackageAmount = newQuote.PackageAmount,
            WeightUnit = newQuote.WeightUnit,
            WeightValue = newQuote.WeightValue,
            CurrencyUnit = newQuote.CurrencyUnit,
            CreatedAt = newQuote.CreatedAt,
            UpdatedAt = newQuote.UpdatedAt,
            DeletedAt = newQuote.DeletedAt,
            IsDeleted = newQuote.IsDeleted
        };

        return new ApiResponse<QuoteDto>
        {
            IsSuccess = true,
            StatusCode = 201,
            Data = newQuoteDto,
            Message = "Quote created",
            Errors = Array.Empty<string>()
        };
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse<Quote>> UpdateQuote(int id, Quote quote)
    {
        if (id != quote.Id)
        {
            return new ApiResponse<Quote>
            {
                IsSuccess = false,
                StatusCode = 400,
                Data = null,
                Message = "Id mismatch",
                Errors = new[] { "Id mismatch" }
            };
        }

        quote.UpdatedAt = DateTime.UtcNow;

        await _quoteService.UpdateAsync(quote);

        return new ApiResponse<Quote>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = quote,
            Message = "Quote updated",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse<Quote>> DeleteQuote(int id)
    {
        var quote = await _quoteService.GetByIdAsync(id);

        if (quote is null)
        {
            return new ApiResponse<Quote>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Quote not found",
                Errors = new[] { "Quote not found" }
            };
        }

        quote.UpdatedAt = DateTime.UtcNow;
        quote.DeletedAt = DateTime.UtcNow;
        quote.IsDeleted = true;

        await _quoteService.UpdateAsync(quote);

        return new ApiResponse<Quote>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = quote,
            Message = "Quote deleted",
            Errors = Array.Empty<string>()
        };
    }

    [HttpDelete("hard/{id}")]
    public async Task<ApiResponse<Quote>> HardDeleteQuote(int id)
    {
        var quote = await _quoteService.GetByIdAsync(id);

        if (quote is null)
        {
            return new ApiResponse<Quote>
            {
                IsSuccess = false,
                StatusCode = 404,
                Data = null,
                Message = "Quote not found",
                Errors = new[] { "Quote not found" }
            };
        }

        await _quoteService.DeleteAsync(id);

        return new ApiResponse<Quote>
        {
            IsSuccess = true,
            StatusCode = 200,
            Data = quote,
            Message = "Quote hard deleted",
            Errors = Array.Empty<string>()
        };
    }

    private async Task<decimal> CalculatePriceForOfferFromQuote(Quote quote)
    {
        decimal basePrice = 1000m;
        decimal price = basePrice;

        switch (quote.Mode)
        {
            case "LCL":
                price *= 1.2m;
                break;
            case "FCL":
                price *= 1.5m;
                break;
            case "Air":
                price *= 2.0m;
                break;
        }

        switch (quote.MovementType)
        {
            case "Door to Door":
                price *= 1.3m;
                break;
            case "Port to Door":
                price *= 1.2m;
                break;
            case "Door to Port":
                price *= 1.2m;
                break;
            case "Port to Port":
                price *= 1.1m;
                break;
        }

        switch (quote.Incoterms)
        {
            case "Delivered Duty Paid":
                price *= 1.2m;
                break;
            case "Delivered At Place":
                price *= 1.1m;
                break;
        }

        decimal countryFactor = await GetCountryFactor(quote.CountryId);
        decimal cityFactor = await GetCityFactor(quote.CityId);
        price *= countryFactor * cityFactor;

        switch (quote.PackageType)
        {
            case "Pallets":
                price *= 1.3m;
                break;
            case "Boxes":
                price *= 1.2m;
                break;
            case "Cartons":
                price *= 1.1m;
                break;
        }

        decimal weightInKg = quote.WeightUnit == "lb" ? quote.WeightValue * 0.45359237m : quote.WeightValue;
        price += weightInKg * 0.5m; // Add $0.5 per kg

        price *= 1 + (quote.PackageAmount * 0.05m); // 5% increase per package

        switch (quote.CurrencyUnit)
        {
            case "EUR":
                price *= 0.85m;
                break; // Assume 1 USD = 0.85 EUR
            case "TRY":
                price *= 30m;
                break; // Assume 1 USD = 30 TRY
        }

        return Math.Round(price, 2);
    }

    private async Task<decimal> GetCountryFactor(int countryId)
    {
        switch (countryId)
        {
            case 1: return 1.0m; // Türkiye
            case 2: return 1.2m; // United States
            case 3: return 1.1m; // United Kingdom
            // ...
            default: return 1.0m;
        }
    }

    private async Task<decimal> GetCityFactor(int cityId)
    {
        switch (cityId)
        {
            case 1: return 1.2m; // İstanbul
            case 11: return 1.3m; // New York
            case 21: return 1.25m; // London
            // ...
            default: return 1.0m;
        }
    }
}