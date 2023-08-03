namespace Qowaiv.Validation.Guarding;

/// <summary>Extension method to define guarding conditions.</summary>
public static class Guarding
{
    /// <summary>Creates a <see cref="Must{TSubject}"/> for this object.</summary>
    /// <typeparam name="TSubject">
    /// The type of the subject.
    /// </typeparam>
    [Pure]
    public static Must<TSubject> Must<TSubject>(this TSubject @this) where TSubject : class
        => new(@this);
}
