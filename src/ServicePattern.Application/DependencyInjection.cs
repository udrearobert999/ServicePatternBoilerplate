using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ServicePattern.Application.Validation;

namespace ServicePattern.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var currentAssembly = AssemblyReference.Assembly;

        services.Scan(scan => scan
            .FromAssemblies(currentAssembly)
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithTransientLifetime());

        services.AddAutoMapper(currentAssembly);
        
        services.AddValidatorsFromAssembly(currentAssembly, ServiceLifetime.Transient);

        services.AddTransient<IValidationOrchestrator, ValidationOrchestrator>();

        return services;
    }
}