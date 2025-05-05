using Contracts.Interfaces.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> 
    where TEntity : class
    where TKey : notnull
{
    private readonly DbSet<TEntity> _entities;

    public Repository(ApplicationDbContext context)
    {
        _entities = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken)
    {
        return await _entities.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _entities.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _entities.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity)
    {
        _entities.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _entities.Remove(entity);
    }
}