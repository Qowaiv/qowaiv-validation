﻿using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Qowaiv.Validation.Abstractions.Internals
{
    /// <summary>Allows the debugger to display collections.</summary>
    [ExcludeFromCodeCoverage]
    internal class CollectionDebugView
    {
        /// <summary>Initializes a new instance of the <see cref="CollectionDebugView"/> class.</summary>
        public CollectionDebugView(IEnumerable enumeration) => _enumeration = enumeration;

        /// <summary>A reference to the enumeration to display.</summary>
        private readonly IEnumerable _enumeration;

        /// <summary>The array that is shown by the debugger.</summary>
        /// <remarks>
        /// Every time the enumeration is shown in the debugger, a new array is created.
        /// By doing this, it is always in sync with the current state of the enumeration.
        /// </remarks>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
#pragma warning disable S2365 // Properties should not make collection or array copies
        // The only way to get the behavior while debugging.
        public object[] Items => _enumeration.Cast<object>().ToArray();
#pragma warning restore S2365 // Properties should not make collection or array copies
    }
}
