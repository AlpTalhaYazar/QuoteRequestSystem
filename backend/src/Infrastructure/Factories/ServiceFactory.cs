using QuoteRequestSystem.Application.Services;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;

namespace QuoteRequestSystem.Infrastructure.Factories;

public class ServiceFactory(IUnitOfWork unitOfWork) : IServiceFactory
{
    public ICityService CreateCityService()
    {
        return new CityService(unitOfWork);
    }

    public ICountryService CreateCountryService()
    {
        return new CountryService(unitOfWork);
    }

    public IDimensionService CreateDimensionService()
    {
        return new DimensionService(unitOfWork);
    }

    public IOfferService CreateOfferService()
    {
        return new OfferService(unitOfWork);
    }

    public IQuoteService CreateQuoteService()
    {
        return new QuoteService(unitOfWork);
    }

    public IUserService CreateUserService()
    {
        return new UserService(unitOfWork);
    }
}