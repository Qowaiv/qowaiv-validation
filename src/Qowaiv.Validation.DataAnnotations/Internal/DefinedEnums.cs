using System.Collections.Frozen;
using System.Reflection;

namespace Qowaiv.Validation.DataAnnotations;

internal abstract class DefinedEnums(Type enumType)
{
    [Pure]
    public static DefinedEnums New(Type enumType, bool onlyAllowDefinedFlags)
        => onlyAllowDefinedFlags
        || !enumType.GetCustomAttributes<FlagsAttribute>().Any()
        ? new Frozen(enumType)
        : new Flags(enumType);

    public Type EnumType { get; } = enumType;

    [Pure]
    public abstract bool Contains(object value);

    [DebuggerDisplay("Type = {EnumType}, Count = {Values.Count}")]
    private sealed class Frozen(Type enumType) : DefinedEnums(enumType)
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private readonly FrozenSet<object> Values = new HashSet<object>(Enum.GetValues(enumType).Cast<object>()).ToFrozenSet();

        [Pure]
        public override bool Contains(object value) => Values.Contains(value);
    }

    [DebuggerDisplay("Type = {EnumType}, Mask = {Mask}")]
    private sealed class Flags(Type enumType) : DefinedEnums(enumType)
    {
        private readonly dynamic Mask = Init(enumType);

        [Pure]
        public override bool Contains(object value)
        {
            dynamic dyn = value;
            return (Mask & dyn) == dyn;
        }

        [Pure]
        private static dynamic Init(Type enumType)
        {
            if (array(Enum.GetValues(enumType).Cast<dynamic>()) is not { Length: > 0 } defined) return 0;

            var mask = defined[0];

            for (var i = 1; i < defined.Length; i++)
            {
                mask |= defined[i];
            }
            return mask;
        }
    }
}
