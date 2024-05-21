using ServicePattern.Application.Results.Constants;

namespace ServicePattern.Application.Results.Errors;

internal record NullError() : Error(ErrorCodes.NullError, nameof(ErrorCodes.NullError));