using Microsoft.EntityFrameworkCore;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Infrastructure.Persistence.Specifications;

internal sealed class SpecificationQueryBuilder<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
        ISpecification<TEntity, TKey> specification)
    {
        var query = inputQuery;

        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        query = specification.Includes.Aggregate(query,
            (current, include) => current.Include(include));

        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        if (specification.IsPagingEnabled)
        {
            var skip = (specification.Page - 1) * specification.PageSize;
            var take = specification.PageSize;

            query = query
                .Skip(skip)
                .Take(take);
        }

        return query;
    }
}