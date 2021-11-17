namespace Qowaiv.Validation.Abstractions.Internals;

[DebuggerDisplay("{DebuggerDisplay}")]
[DebuggerTypeProxy(typeof(CollectionDebugView))]
internal class FixedMessages : IReadOnlyList<IValidationMessage>
{
    [Pure]
    public static FixedMessages New(IEnumerable<IValidationMessage> messages)
        => messages is FixedMessages @internal
            ? @internal
            : Empty.AddRange(messages);

    public static readonly FixedMessages Empty = new();

    public virtual int Count => 0;

    [Pure]
    public FixedMessages Add(IValidationMessage message)
        => message.Severity > ValidationSeverity.None
        ? new SomeFixedMessages(this, message)
        : this;

    [Pure]
    public FixedMessages AddRange(IEnumerable<IValidationMessage> elements)
    {
        var next = this;
        foreach (var element in elements)
        {
            next = next.Add(element);
        }
        return next;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay => $"Count = {Count}";

    public IValidationMessage this[int index] => this.Skip(index).FirstOrDefault() ?? throw new IndexOutOfRangeException();

    [Pure]
    public virtual IEnumerator<IValidationMessage> GetEnumerator() => Enumerable.Empty<IValidationMessage>().GetEnumerator();

    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
