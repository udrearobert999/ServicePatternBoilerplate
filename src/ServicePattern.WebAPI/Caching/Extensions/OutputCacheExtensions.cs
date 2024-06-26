using Microsoft.AspNetCore.OutputCaching;
using ServicePattern.Application.Dtos.Movies.Options;
using ServicePattern.Application.Shared.Helpers;
using ServicePattern.WebAPI.Caching.Constants;

namespace ServicePattern.WebAPI.Caching.Extensions;

internal static class OutputCacheExtensions
{
    public static OutputCacheOptions ConfigureCustomPolicies(this OutputCacheOptions options)
    {
        options.AddBasePolicy(policy => policy.Cache());
        options.AddPolicy(CacheConstants.Policies.Movies, policy =>
            policy.Cache()
                .Expire(TimeSpan.FromMinutes(1))
                .SetVaryByQueryByTypeProperties<GetAllMoviesOptionsDto>()
                .Tag(CacheConstants.Keys.Movies));

        return options;
    }

    public static OutputCachePolicyBuilder SetVaryByQueryByTypeProperties<T>(this OutputCachePolicyBuilder builder)
    {
        var jsonPropertiesOfType = ReflectionHelper.GetProperties<T>();

        builder.SetVaryByQuery(jsonPropertiesOfType);

        return builder;
    }
}