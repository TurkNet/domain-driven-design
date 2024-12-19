using System.Reflection;

namespace Tn.Inventory.Domain;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}