using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations.Reflection;

internal static class ObjectWrapper
{
    extension(object obj)
    {
        /// <summary>Tries to resolve the value of the length property.</summary>
        [Pure]
        public long? TryLength() => obj.Try("Length");

        /// <summary>Tries to resolve the value of the count property.</summary>
        [Pure]
        public long? TryCount() => obj.Try("Count");

        [Pure]
        private long? Try(string name) => obj switch
        {
            string str => str.Length,
            Array array => array.Length,
            ICollection collection => collection.Count,
            _ when obj.GetValue(name) is { } value => value,
            _ => null,
        };

        [Pure]
        private long? GetValue(string name)
        {
            if (obj.GetType().GetRuntimeProperty(name) is { CanRead: true } prop)
            {
                return prop.PropertyType switch
                {
                    var type when type == typeof(int) => (int?)prop.GetValue(obj),
                    var type when type == typeof(long) => (long?)prop.GetValue(obj),
                    _ => null,
                };
            }
            else return null;
        }
    }
}
