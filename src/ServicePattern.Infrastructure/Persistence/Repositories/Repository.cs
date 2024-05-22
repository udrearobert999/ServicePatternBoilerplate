﻿using Microsoft.EntityFrameworkCore;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Infrastructure.Persistence.Repositories;

internal class Repository<TEntity, TKey> : ReadOnlyRepository<TEntity, TKey>, IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    public Repository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var createdEntity = await _dbSet.AddAsync(entity, cancellationToken);

        return createdEntity.Entity;
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var updatedEntity = _dbSet.Update(entity);

        return Task.FromResult(updatedEntity.Entity);
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _dbSet.Remove(entity);

        return Task.CompletedTask;
    }
}