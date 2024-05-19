using System.Text.Json.Serialization;

namespace ServicePattern.Presentation.Helpers;

internal class ReflectionHelper
{
    public static string[] GetProperties<T>()
    {
        return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(prop => prop.Name)
            .ToArray();
    }
}