using System.Linq.Expressions;
using ServicePattern.Domain.Entities;

namespace ServicePattern.Domain.Abstractions;

public abstract class Specification<TEntity, TKey> : ISpecification<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct

{
    public Expression<Func<TEntity, bool>>? Criteria { get; }
    public List<Expression<Func<TEntity, object>>> Includes { get; } = new();
    public Expression<Func<TEntity, object>>? OrderBy { get; private set; }
    public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }
    public Expression<Func<TEntity, object>>? GroupBy { get; private set; }

    public int Page { get; private set; }
    public int PageSize { get; private set; }
    public bool IsPagingEnabled { get; private set; }

    protected Specification()
    {
    }

    protected Specification(Expression<Func<TEntity, bool>>? criteria)
    {
        Criteria = criteria;
    }

    protected virtual void AddInclude(Expression<Func<TEntity, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }

    protected virtual void ApplyPaging(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
        IsPagingEnabled = true;
    }

    protected virtual void ApplyOrderBy(Expression<Func<TEntity, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }

    protected virtual void ApplyOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }

    protected virtual void ApplyGroupBy(Expression<Func<TEntity, object>> groupByExpression)
    {
        GroupBy = groupByExpression;
    }
}