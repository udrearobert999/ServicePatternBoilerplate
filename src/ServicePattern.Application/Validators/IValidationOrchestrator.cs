using ServicePattern.Application.Dtos.Result;

namespace ServicePattern.Application.Validators;

internal interface IValidationOrchestrator
{
    public Task<Result> ValidateAsync<TEntity>(TEntity entity,
        CancellationToken cancellationToken = default) where TEntity : class;
}