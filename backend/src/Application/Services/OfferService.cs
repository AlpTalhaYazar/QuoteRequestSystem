using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Application.Services;

public class OfferService : GenericService<Offer>, IOfferService
{
    private readonly IUnitOfWork _unitOfWork;

    public OfferService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}