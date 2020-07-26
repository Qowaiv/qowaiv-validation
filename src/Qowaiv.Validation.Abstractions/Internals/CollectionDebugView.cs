using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Qowaiv.Validation.Abstractions.Internals
{
    /// <summary>Allows the debugger to display collections.</summary>
    [ExcludeFromCodeCoverage]
    internal class CollectionDebugView
    {
        /// <summary>Constructor.</summary>
        public CollectionDebugView(IEnumerable enumeration) => Enumeration = enumeration;

        /// <summary>The array that is shown by the debugger.</summary>
        /// <remarks>
        /// Every time the enumeration is shown in the debugger, a new array is created.
        /// By doing this, it is always in sync with the current state of the enumeration.
        /// </remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public object[] Items => Enumeration.Cast<object>().ToArray();

        /// <summary>A reference to the enumeration to display.</summary>
        private readonly IEnumerable Enumeration;
    }
}
