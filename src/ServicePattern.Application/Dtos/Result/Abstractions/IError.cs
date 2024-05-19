namespace ServicePattern.Application.Dtos.Result.Abstractions;

public interface IError
{
    public string Code { get; init; }

    public string Message { get; init; }

    public IEnumerable<IError> InnerErrors { get; set; }
}