using ServicePattern.WebAPI.Endpoints.Abstractions;

namespace ServicePattern.WebAPI.Endpoints.Extensions;

public static class EndpointsMapperExtensions
{
    public static void AddEndpoints(this IServiceCollection services)
    {
        services.Scan(s => s.FromAssemblyOf<Program>()
            .AddClasses(c => c.AssignableTo(typeof(IEndpointsMapper)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }

    public static void UseEndpoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope(); 
        var scopedServices = scope.ServiceProvider;
        
        var endpointsMappers = scopedServices.GetServices<IEndpointsMapper>().ToList();

        endpointsMappers.ForEach(mapper => mapper.MapEndpoints(app));

        
    }
}