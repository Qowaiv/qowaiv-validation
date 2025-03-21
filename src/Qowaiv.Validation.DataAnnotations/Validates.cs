namespace Qowaiv.Validation.DataAnnotations;

internal static class Validates
{
    public static void Model(NestedContext context)
    {
        // instance has not been validated yet.
        if (!context.Visited(context.Instance))
        {
            Members(context);
            Type(context);
            ValidatableObject(context);
        }
    }

    /// <summary>Gets the results for validating the (annotated) members.</summary>
    public static void Members(NestedContext context)
    {
        if (context.Annotations is not { } annotations) { return; }

        foreach (var property in annotations.Members)
        {
            Member(context, property);
        }
    }

    /// <summary>Gets the results for validating a single annotated property.</summary>
    /// <remarks>
    /// It creates a sub validation context.
    /// </remarks>
    private static void Member(NestedContext context, MemberAnnotations annotations)
    {
        if (!context.TryMember(annotations, out var value)) { return; }

        foreach (var attribute in annotations.Attributes)
        {
            // Stop on first required failure.
            if (context.AddMessage(attribute.GetValidationMessage(value, context)) && attribute is RequiredAttribute)
            {
                return;
            }
        }

        if (value is null || annotations.TypeAnnotations is not { } typeAnnotations) { return; }

        if (value is IEnumerable enumerable)
        {
            var index = -1;
            foreach (var item in enumerable)
            {
                index++;
                if (item is null) { continue; }

                Model(context.Nested(item, typeAnnotations, index));
            }
        }
        else
        {
            Model(context.Nested(value, typeAnnotations));
        }
    }

    /// <summary>Gets the results for validating the attributes declared on the type of the model.</summary>
    public static void Type(NestedContext context)
    {
        if (context.Annotations?.Attributes is { Count: > 0 } attributes)
        {
            foreach (var attribute in attributes)
            {
                context.AddMessage(attribute.GetValidationMessage(context.Instance, context), violationOnType: true);
            }
        }
    }

    /// <summary>Gets the results for validating <see cref="IValidatableObject.Validate(ValidationContext)"/>.</summary>
    /// <remarks>
    /// If the model is not <see cref="IValidatableObject"/> nothing is done.
    /// </remarks>
    public static void ValidatableObject(NestedContext context)
    {
        if (context.Instance is IValidatableObject validatable)
        {
            context.AddMessages(validatable.Validate(context));
        }
    }
}
