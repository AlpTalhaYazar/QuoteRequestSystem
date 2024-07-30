using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;
using QuoteRequestSystem.Domain.Entities;

namespace QuoteRequestSystem.Application.Services;

public class DimensionService : GenericService<Dimension>, IDimensionService
{
    private readonly IUnitOfWork _unitOfWork;

    public DimensionService(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}