using ServicePattern.Domain.Results.Constants;

namespace ServicePattern.Domain.Results.Errors;

internal record NullError() : Error(ErrorCodes.NullError, nameof(ErrorCodes.NullError));