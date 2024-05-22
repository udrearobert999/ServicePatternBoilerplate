using Microsoft.AspNetCore.OutputCaching;
using ServicePattern.Application.Shared.Helpers;

namespace ServicePattern.Presentation.Extension;

internal static class OutputCacheExtension
{
    public static OutputCachePolicyBuilder SetVaryByQueryByTypeProperties<T>(this OutputCachePolicyBuilder builder)
    {
        var jsonPropertiesOfType = ReflectionHelper.GetProperties<T>();

        builder.SetVaryByQuery(jsonPropertiesOfType);

        return builder;
    }
}