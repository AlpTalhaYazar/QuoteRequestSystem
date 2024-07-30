using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QuoteRequestSystem.Core.Repositories;
using QuoteRequestSystem.Infrastructure.Data;

namespace QuoteRequestSystem.Infrastructure.Repositories;

public class GenericRepository<T>(ApplicationDbContext context) : IGenericRepository<T>
    where T : class
{
    protected readonly ApplicationDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null)
        {
            throw new Exception($"{typeof(T).Name} with id {id} not found.");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<int> ids)
    {
        var entities = await _dbSet.Where(e =>
            ids.Contains((int)e.GetType().GetProperties().Where(p => p.Name == "Id").Select(p => p.GetValue(e))
                .FirstOrDefault())).ToListAsync();

        if (entities == null)
        {
            throw new Exception($"No {typeof(T).Name} found.");
        }

        return entities;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        var entities = await _dbSet.ToListAsync();

        if (entities == null)
        {
            throw new Exception($"No {typeof(T).Name} found.");
        }

        return entities;
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var entities = await _dbSet.Where(predicate).ToListAsync();

        if (entities == null)
        {
            throw new Exception($"No {typeof(T).Name} found.");
        }

        return entities;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);

        return entity;
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        _dbSet.Remove(entity);

        await Task.CompletedTask;
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);

        await Task.CompletedTask;
    }
}