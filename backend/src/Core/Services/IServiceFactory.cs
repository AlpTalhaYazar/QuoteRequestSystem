namespace QuoteRequestSystem.Core.Services;

public interface IServiceFactory
{
    ICityService CreateCityService();
    ICountryService CreateCountryService();
    IDimensionService CreateDimensionService();
    IOfferService CreateOfferService();
    IQuoteService CreateQuoteService();
    IUserService CreateUserService();
}