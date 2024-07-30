using System.Linq.Expressions;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Core.Services;
using QuoteRequestSystem.Core.UnitOfWork;

namespace QuoteRequestSystem.Application.Services;

public class GenericService<T> : IGenericService<T> where T : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<T> _genericRepository;

    public GenericService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _genericRepository = _unitOfWork.GetRepository<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids)
    {
        return _genericRepository.GetByIdsAsync(ids);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _genericRepository.GetAllAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _genericRepository.FindAsync(predicate);
    }

    public async Task<T> AddAsync(T entity)
    {
        var entityCreated = await _genericRepository.AddAsync(entity);

        await _unitOfWork.SaveChangesAsync();

        return entityCreated;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _genericRepository.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(T entity)
    {
        await _genericRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _genericRepository.DeleteAsync(id);
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        await _genericRepository.DeleteRangeAsync(entities);
    }
}