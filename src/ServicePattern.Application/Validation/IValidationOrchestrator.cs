using ServicePattern.Domain.Results;

namespace ServicePattern.Application.Validation;

internal interface IValidationOrchestrator
{
    public Task<Result> ValidateAsync<TEntity>(TEntity entity,
        CancellationToken cancellationToken = default) where TEntity : class;
}