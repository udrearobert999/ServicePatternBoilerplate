using System.Reflection;

namespace ServicePattern.Persistance;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}