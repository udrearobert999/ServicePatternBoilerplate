﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServicePattern.Application.Dtos.Result;
using ServicePattern.Application.Dtos.Result.Abstractions;
using ServicePattern.Application.Dtos.Result.Errors.Factory;

namespace ServicePattern.Application.Validators;

internal sealed class ValidationOrchestrator : IValidationOrchestrator
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationOrchestrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<Result> ValidateAsync<TEntity>(TEntity entity,
        CancellationToken cancellationToken = default) where TEntity : class
    {
        var validators = _serviceProvider.GetServices<IValidator<TEntity>>();

        var errors = new List<IError>();
        foreach (var validator in validators)
        {
            var validationResult = await validator.ValidateAsync(entity, cancellationToken);

            var validatorErrors = validationResult.Errors
                .Where(failure => failure != null)
                .Select(failure => ErrorFactory.ValidationFailure(failure.PropertyName, failure.ErrorMessage))
                .Distinct()
                .ToList();

            errors.AddRange(validatorErrors);
        }

        if (errors.Any())
        {
            var validationFailureError = ErrorFactory.ValidationFailure(errors);

            return Result.FromError(validationFailureError);
        }

        return Result.Success();
    }
}