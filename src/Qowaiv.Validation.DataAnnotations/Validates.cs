namespace Qowaiv.Validation.DataAnnotations;

internal static class Validates
{
    public static void Model(NestedContext context)
    {
        // instance has not been validated yet.
        if (!context.Visited(context.Instance))
        {
            Properties(context);
            Type(context);
            ValidatableObject(context);
        }
    }

    /// <summary>Gets the results for validating the (annotated )properties.</summary>
    public static void Properties(NestedContext context)
    {
        if (context.Annotations is not { } annotations) { return; }

        foreach (var property in annotations.Properties)
        {
            Property(context, property);
        }
    }

    /// <summary>Gets the results for validating a single annotated property.</summary>
    /// <remarks>
    /// It creates a sub validation context.
    /// </remarks>
    private static void Property(NestedContext context, PropertyAnnotations annotations)
    {
        if (!context.TryProperty(annotations, out var value)) { return; }

        context.Buffer.Clear();

        Validator.TryValidateValue(value, context, context.Buffer, annotations.Attributes);

        context.AddMessages(context.Buffer);

        

        //// Only validate the other properties if the required condition was not met.
        //if (annotations.Required is not OptionalAttribute &&
        //    context.AddMessage(annotations.Required.GetValidationMessage(value, context)))
        //{
        //    return;
        //}

        //foreach (var attribute in annotations.Attributes)
        //{
        //    context.AddMessage(attribute.GetValidationMessage(value, context));
        //}

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
            context.Buffer.Clear();
            Validator.TryValidateValue(context.Instance, context, context.Buffer, attributes);
            context.AddMessages(context.Buffer, violationOnType: true);
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
