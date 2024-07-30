using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Application.Services;

public class QuoteService : GenericService<Quote>, IQuoteService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuoteService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}