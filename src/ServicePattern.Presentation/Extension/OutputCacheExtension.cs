using Microsoft.AspNetCore.OutputCaching;
using ServicePattern.Presentation.Helpers;

namespace ServicePattern.Presentation.Extension;

internal static class OutputCacheExtension
{
    public static OutputCachePolicyBuilder SetVaryByQueryByTypeProps<T>(this OutputCachePolicyBuilder builder)
    {
        var jsonPropertiesOfType = ReflectionHelper.GetProperties<T>();

        builder.SetVaryByQuery(jsonPropertiesOfType);

        return builder;
    }
}