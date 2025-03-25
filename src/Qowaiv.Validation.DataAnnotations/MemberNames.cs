namespace Qowaiv.Validation.DataAnnotations;

[DebuggerTypeProxy(typeof(Diagnostics.CollectionDebugView<string>))]
[DebuggerDisplay("Count = {Count}")]
internal readonly struct MemberNames : IReadOnlyCollection<string>
{
    public MemberNames(string memberName)
    {
        Path = MemberPath.Root.Nested(memberName);
        Members = new(0);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly MemberPath Path;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private readonly List<string> Members;

    /// <inheritdoc />
    public int Count => Members.Count;

    public void AddIndex(int index) => Members.Add(Path.Index(index));

    /// <inheritdoc />
    [Pure]
    public IEnumerator<string> GetEnumerator() => Members.GetEnumerator();

    /// <inheritdoc />
    [Pure]
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
