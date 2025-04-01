namespace Qowaiv.Validation.DataAnnotations;

internal static class Validate
{
    public static void Model(Nested nested, ValidateContext ctx)
    {
        // instance has not been validated yet.
        if (!ctx.Done.Add(nested.Instance)) return;

        (var instance, var annotations, var path) = nested;

        var context = ctx.Validation(nested);

        foreach (var member in annotations.Members)
        {
            Member(member, nested, ctx, context);
        }

        if (annotations.CheckEnumerable && instance is IEnumerable enumerable)
        {
            var enumTyped = TypeAnnotations.Get(enumerable.GetType().GetEnumerableType()!);

            // We have to reset it if the type is not sealed.
            if (enumTyped?.CheckInheritance is true) enumTyped = null;

            var index = -1;

            foreach (var item in enumerable)
            {
                index++;
                if (item is null || (enumTyped ?? TypeAnnotations.Get(item.GetType())) is not { } typed) continue;

                Model(new Nested(item, typed, path.Child(index)), ctx);
            }
        }

        if (annotations.CheckValidatable && instance is IValidatableObject validatable)
        {
            // Reset validation context
            context.MemberName = null;
            context.DisplayName = context.ObjectType.Name;

            ctx.AddMessages(validatable.Validate(context), path);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void Member(MemberAnnotations member, Nested nested, ValidateContext ctx, ValidationContext context)
    {
        object? value;

        try { value = member.GetValue(nested.Instance); }
        catch
        {
            ctx.AddMessage(ValidationMessage.Error($"The value is inaccessible.", member.Name), nested.Path);
            return;
        }

        context.MemberName = member.Name;
        context.DisplayName = member.Display?.GetName() ?? member.Name;

        foreach (var attribute in member.Attributes)
        {
            // Stop on first required failure and do not validate further.
            if (ctx.AddMessage(attribute.GetValidationMessage(value, context), nested.Path) && attribute is RequiredAttribute)
            {
                return;
            }
        }

        if (value is { } && TypeAnnotations.Get(value.GetType()) is { CheckRecursive: true } typed)
        {
            Model(new Nested(value, typed, nested.Path.Child(member.Name)), ctx);
        }
    }
}
