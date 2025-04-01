namespace Qowaiv.Validation.DataAnnotations;

internal static class Validate
{
    public static void Model(Nested nested, ValidateContext ctx)
    {
        // instance has not been validated yet.
        if (!ctx.Done.Add(nested.Instance)) return;

        (var instance, var annotations, var path) = nested;

        var context = ctx.Validation(nested.Instance);

        foreach (var member in annotations.Members)
        {
            Member(member, nested, ctx, context);
        }

        if (annotations.CheckEnumerable && instance is IEnumerable enumerable)
        {
            var index = -1;

            foreach (var item in enumerable)
            {
                index++;
                if (item is null || TypeAnnotations.Get(item.GetType().GetEnumerableType()!) is not { } typed) continue;

                var child = new Nested(item, typed, path.Child(index));
                Model(child, ctx);
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
            var child = new Nested(value, typed, nested.Path.Child(member.Name));
            Model(child, ctx);
        }
    }
}
