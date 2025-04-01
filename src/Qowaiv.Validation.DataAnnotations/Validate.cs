namespace Qowaiv.Validation.DataAnnotations;

internal static class Validate
{
    public static void Model(NestedContext context)
    {
        // instance has not been validated yet.
        if (context.Visited(context.Instance)) return;

        var annotations = context.Annotations;

        foreach (var member in annotations.Members)
        {
            Member(context, member);
        }

        if (annotations.CheckValidatable && context.Instance is IValidatableObject validatable)
        {
            context.AddMessages(validatable.Validate(context));
        }
    }

    /// <summary>Gets the results for validating a single annotated property.</summary>
    /// <remarks>
    /// It creates a sub validation context.
    /// </remarks>
    private static void Member(NestedContext context, MemberAnnotations memberAnnotations)
    {
        if (MemberAttributes(context, memberAnnotations) is not { } value) { return; }
        if (TypeAnnotations.Get(value.GetType()) is not { } annotations) { return; }

        if (annotations.CheckEnumerable && value is IEnumerable enumerable)
        {
            var index = -1;
            foreach (var item in enumerable)
            {
                index++;
                if (item is null) { continue; }

                //(context.Nested(item, annotations, index))
            }
        }
        if (annotations.CheckRecursive)
        {
            Model(context.Nested(value, annotations));
        }
    }

    [Impure]
    private static object? MemberAttributes(NestedContext context, MemberAnnotations annotations)
    {
        if (!context.TryMember(annotations, out var value)) { return null; }

        foreach (var attribute in annotations.Attributes)
        {
            // Stop on first required failure and do not validate further.
            if (context.AddMessage(attribute.GetValidationMessage(value, context)) && attribute is RequiredAttribute)
            {
                return null;
            }
        }
        return value;
    }
}
