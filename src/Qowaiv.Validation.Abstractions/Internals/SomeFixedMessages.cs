﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Qowaiv.Validation.Abstractions.Internals
{
    internal class SomeFixedMessages : FixedMessages
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly FixedMessages parent;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IValidationMessage value;

        public SomeFixedMessages(FixedMessages parent, IValidationMessage value)
        {
            this.parent = parent;
            this.value = value;
        }

        public override int Count => parent.Count + 1;

        public override IEnumerator<IValidationMessage> GetEnumerator() => Enumerate().Reverse().GetEnumerator();

        private IEnumerable<IValidationMessage> Enumerate()
        {
            FixedMessages current = this;
            while (current is SomeFixedMessages nonEmpty)
            {
                yield return nonEmpty.value;
                current = nonEmpty.parent;
            }
        }
    }
}
