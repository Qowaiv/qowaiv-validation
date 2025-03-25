namespace Qowaiv.Validation.DataAnnotations.Diagnostics;

/// <summary>Allows the debugger to display collections.</summary>
[ExcludeFromCodeCoverage]
internal sealed class CollectionDebugView<T>(IEnumerable<T> enumeration)
{
    /// <summary>A reference to the enumeration to display.</summary>
    private readonly IEnumerable<T> _enumeration = enumeration;

    /// <summary>The array that is shown by the debugger.</summary>
    /// <remarks>
    /// Every time the enumeration is shown in the debugger, a new array is created.
    /// By doing this, it is always in sync with the current state of the enumeration.
    /// </remarks>
    [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
#pragma warning disable S2365 // Properties should not make collection or array copies
    // The only way to get the behavior while debugging.
    public T[] Items => [.. _enumeration];
}
