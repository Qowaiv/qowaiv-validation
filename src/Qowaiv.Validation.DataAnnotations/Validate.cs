namespace Qowaiv.Validation.DataAnnotations;

internal static class Validate
{
    public static void Model(NestedContext context)
    {
        // instance has not been validated yet.
        if (!context.Visited(context.Instance))
        {
            Members(context);
            IValidatableObject(context);
        }
    }

    /// <summary>Gets the results for validating the (annotated) members.</summary>
    public static void Members(NestedContext context)
    {
        if (context.Annotations is not { } annotations) return;

        foreach (var member in annotations)
        {
            Member(context, member);
        }
    }

    /// <summary>Gets the results for validating a single annotated property.</summary>
    /// <remarks>
    /// It creates a sub validation context.
    /// </remarks>
    private static void Member(NestedContext context, MemberAnnotations annotations)
    {
        if (MemberAttributes(context, annotations) is not { } value ||
             MemberAnnotations.Get(value.GetType()) is not { } memberAnnotations) { return; }

        if (value is IEnumerable enumerable)
        {
            var index = -1;
            foreach (var item in enumerable)
            {
                index++;
                if (item is null) { continue; }

                Model(context.Nested(item, memberAnnotations, index));
            }
        }
        else
        {
            Model(context.Nested(value, memberAnnotations));
        }
    }

    [Impure]
    private static object? MemberAttributes(NestedContext context, MemberAnnotations annotations)
    {
        if (!context.TryMember(annotations, out var value)) { return null; }

        foreach (var attribute in annotations.Attributes)
        {
            // Stop on first required failure.
            if (context.AddMessage(attribute.GetValidationMessage(value, context)) && attribute is RequiredAttribute)
            {
                return value;
            }
        }
        return value;
    }

    /// <summary>Gets the results for validating <see cref="IValidatableObject.Validate(ValidationContext)"/>.</summary>
    /// <remarks>
    /// If the model is not <see cref="System.ComponentModel.DataAnnotations.IValidatableObject"/> nothing is done.
    /// </remarks>
    public static void IValidatableObject(NestedContext context)
    {
        if (context.Instance is IValidatableObject validatable)
        {
            context.AddMessages(validatable.Validate(context));
        }
    }
}
