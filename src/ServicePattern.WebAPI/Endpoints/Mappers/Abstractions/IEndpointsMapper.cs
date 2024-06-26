namespace ServicePattern.WebAPI.Endpoints.Abstractions;

public interface IEndpointsMapper
{
    public void MapEndpoints(IEndpointRouteBuilder builder);
}