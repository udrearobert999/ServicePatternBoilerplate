using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicePattern.Domain.Abstractions;
using ServicePattern.Infrastructure.Persistence;

namespace ServicePattern.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")!;
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddScoped<DbContext, AppDbContext>();

        services.Scan(scan => scan
            .FromAssemblies(AssemblyReference.Assembly)
            .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
            .AsImplementedInterfaces());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}