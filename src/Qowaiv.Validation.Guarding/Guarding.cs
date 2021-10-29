using System.Diagnostics.Contracts;

namespace Qowaiv.Validation.Guarding
{
    /// <summary>Extension method
    /// 
    /// </summary>
    public static class Guarding
    {
        /// <summary>Creates a <see cref="Validation.Guarding.Must{TSubject}"/> for this object.</summary>
        /// <typeparam name="TSubject">
        /// The type of the 
        /// </typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        [Pure]
        public static Must<TSubject> Must<TSubject>(this TSubject @this) where TSubject : class
            => new(@this);
    }
}
