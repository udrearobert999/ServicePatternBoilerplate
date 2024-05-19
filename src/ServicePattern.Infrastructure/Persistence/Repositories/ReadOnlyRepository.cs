using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;
using ServicePattern.Infrastructure.Persistence.Specifications;

namespace ServicePattern.Infrastructure.Persistence.Repositories;

internal class ReadOnlyRepository<TEntity, TKey> : IReadOnlyRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    public ReadOnlyRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(TKey key, bool track = true,
        CancellationToken cancellationToken = default)
    {
        if (track)
            return await _dbSet.FindAsync(key);

        return await _dbSet.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id.Equals(key), cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAll(bool track = true, CancellationToken cancellationToken = default)
    {
        if (track)
            return await _dbSet.ToListAsync(cancellationToken);

        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetBySpecAsync(ISpecification<TEntity, TKey> spec,
        CancellationToken cancellationToken = default)
    {
        return await ApplySpecification(spec).ToListAsync(cancellationToken);
    }

    public Task<TEntity?> GetSingleOrDefaultBySpecAsync(ISpecification<TEntity, TKey> spec,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(spec).SingleOrDefaultAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        return _dbSet.AsNoTracking().AnyAsync(expression, cancellationToken);
    }

    public Task<bool> ExistsBySpecAsync(ISpecification<TEntity, TKey> spec,
        CancellationToken cancellationToken = default)
    {
        return ApplySpecification(spec).AnyAsync(cancellationToken);
    }

    public Task<int> CountAsync(Expression<Func<TEntity, bool>>? expression,
        CancellationToken cancellationToken = default)
    {
        if (expression is null)
            return _dbSet.AsNoTracking().CountAsync(cancellationToken);

        return _dbSet.AsNoTracking().CountAsync(expression, cancellationToken);
    }

    public Task<int> CountBySpecAsync(ISpecification<TEntity, TKey> spec, CancellationToken cancellationToken = default)
    {
        return ApplySpecification(spec).CountAsync(cancellationToken);
    }

    private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity, TKey> spec)
    {
        return SpecificationQueryBuilder<TEntity, TKey>.GetQuery(_dbSet.AsQueryable(), spec);
    }
}