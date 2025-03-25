namespace Qowaiv.Validation.Abstractions.Internals;

internal sealed class SomeFixedMessages(FixedMessages parent, IValidationMessage value) : FixedMessages
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly FixedMessages parent = parent;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly IValidationMessage value = value;

    public override int Count => parent.Count + 1;

    [Pure]
    public override IEnumerator<IValidationMessage> GetEnumerator() => Enumerate().Reverse().GetEnumerator();

    [Pure]
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
