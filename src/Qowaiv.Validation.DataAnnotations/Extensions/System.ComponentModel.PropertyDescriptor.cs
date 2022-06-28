namespace System.ComponentModel
{
    /// <summary>Extensions on <see cref="PropertyDescriptor"/>.</summary>
    public static class QowaivPropertyDescriptorExtensions
    {
        /// <summary>Gets the decorated <see cref="RequiredAttribute"/> for the property.</summary>
        [Pure]
        public static RequiredAttribute GetRequiredAttribute(this PropertyDescriptor descriptor)
            =>  Guard.NotNull(descriptor, nameof(descriptor))
            .Attributes
            .Cast<Attribute>()
            .OfType<RequiredAttribute>()
            .FirstOrDefault(attr => attr is not OptionalAttribute);

        /// <summary>Gets the decorated <see cref="RequiredAttribute"/> for the property.</summary>
        [Pure]
        public static IEnumerable<ValidationAttribute> GetValidationAttributes(this PropertyDescriptor descriptor)
            => Guard.NotNull(descriptor, nameof(descriptor))
            .Attributes
            .Cast<Attribute>()
            .OfType<ValidationAttribute>();

        /// <summary>Gets the decorated <see cref="DisplayAttribute"/> for the property.</summary>
        [Pure]
        public static DisplayAttribute GetDisplayAttribute(this PropertyDescriptor descriptor)
            => (DisplayAttribute)Guard.NotNull(descriptor, nameof(descriptor))
            .Attributes[typeof(DisplayAttribute)];

        /// <summary>Gets the type converter associated with the property.</summary>
        [Pure]
        [Obsolete("Will be droped.")]
        public static TypeConverter GetTypeConverter(this PropertyDescriptor descriptor)
        {
            var converter = Guard.NotNull(descriptor, nameof(descriptor)).Attributes
                .Cast<Attribute>()
                .OfType<TypeConverterAttribute>()
                .Select(att => Type.GetType(att.ConverterTypeName))
                .FirstOrDefault();

            return Activate(converter, descriptor.PropertyType)
                ?? TypeDescriptor.GetConverter(descriptor.PropertyType);

            static TypeConverter Activate(Type converter, Type type)
            {
                if (converter is { })
                {
                    var ctors = converter.GetConstructors();
                    if (ctors.Any(c => !c.GetParameters().Any()))
                    {
                        return (TypeConverter)Activator.CreateInstance(converter);
                    }
                    else if (ctors.Any(c => c.GetParameters().Length == 1 && c.GetParameters()[0].ParameterType == typeof(Type)))
                    {
                        return (TypeConverter)Activator.CreateInstance(converter, type);
                    }
                    else return null;
                }
                else return null;
            }
        }
    }
}
