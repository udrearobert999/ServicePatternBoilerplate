using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;
using ServicePattern.Application.Dtos;
using ServicePattern.Application.Dtos.Movies.Options;
using ServicePattern.Presentation.Config;
using ServicePattern.Presentation.Constants;
using ServicePattern.Presentation.Extension;

namespace ServicePattern.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.Conventions.Add(new RouteTokenTransformerConvention(new SpinalCaseRouteNameTransformer()));
            })
            .AddApplicationPart(AssemblyReference.Assembly);


        services.AddOutputCache(x =>
        {
            x.AddBasePolicy(c => c.Cache());
            x.AddPolicy(CacheConstants.Policies.Movies, c =>
                c.Cache()
                    .Expire(TimeSpan.FromMinutes(1))
                    .SetVaryByQueryByTypeProps<GetAllMoviesOptionsDto>()
                    .Tag(CacheConstants.Keys.Movies));
        });

        return services;
    }
}