using Qowaiv.Validation.DataAnnotations;
using RangeAttribute = NUnit.Framework.RangeAttribute;

namespace Data_annotations.Attributes.Collection_specs;

public class At_least
{
    public class Supports
    {
        [Test]
        public void ICollection() => new Collection.AtLeastAttribute(2).IsValid(new[] { 1, 2 }).Should().BeTrue();

        [Test]
        public void type_with_count_property() => new Collection.AtLeastAttribute(4).IsValid(new MyCollection(17)).Should().BeTrue();
    }

    public class Does_not_support
    {
        [Test]
        public void type_without_count_property()
            => new Collection.AtLeastAttribute(4).Invoking(a => a.IsValid(new object()))
            .Should().Throw<UnsupportedType>().WithMessage("Collection.AtLeastAttribute does not support properties of the type object.");
    }

    public class Ingores
    {
        [Test]
        public void @null() => new Collection.AtLeastAttribute(0).IsValid(null).Should().BeTrue();

        [Test]
        public void length_zero() => new Collection.AtLeastAttribute(4).IsValid(string.Empty).Should().BeTrue();
    }

    [Test]
    public void validates([Range(4, 10)] int value) => new Collection.AtLeastAttribute(4).IsValid(new byte[value]).Should().BeTrue();

    [Test]
    public void Invalidates([Range(1, 10)] int value) => new Collection.AtLeastAttribute(11).IsValid(new byte[value]).Should().BeFalse();

    [TestCase("nl", "Veld AtLeastProp moet tenminste 4 items bevatten.")]
    [TestCase("en", "The AtLeastProp field should have at least 4 items.")]
    public void With_message(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model() {AtLeastProp = "a" }.Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "AtLeastProp"));
    }
}

public class At_most
{
    public class Supports
    {
        [Test]
        public void ICollection() => new Collection.AtMostAttribute(2).IsValid(new[] { 1, 2 }).Should().BeTrue();

        [Test]
        public void type_with_count_property() => new Collection.AtMostAttribute(40).IsValid(new MyCollection(17)).Should().BeTrue();
    }

    public class Does_not_support
    {
        [Test]
        public void type_without_count_property()
            => new Collection.AtMostAttribute(4).Invoking(a => a.IsValid(new object()))
            .Should().Throw<UnsupportedType>().WithMessage("Collection.AtMostAttribute does not support properties of the type object.");
    }

    public class Ingores
    {
        [Test]
        public void @null() => new Collection.AtMostAttribute(0).IsValid(null).Should().BeTrue();
    }

    [Test]
    public void validates([Range(1, 4)] int value) => new Collection.AtMostAttribute(4).IsValid(new byte[value]).Should().BeTrue();

    [Test]
    public void Invalidates([Range(12, 20)] int value) => new Collection.AtMostAttribute(11).IsValid(new byte[value]).Should().BeFalse();

    [TestCase("nl", "Veld AtMostProp mag niet meer dan 4 items bevatten.")]
    [TestCase("en", "The AtMostProp field should have at most 4 items.")]
    public void With_message(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model() { AtMostProp = "abcde" }.Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "AtMostProp"));
    }
}

public class In_range
{
    public class Supports
    {
        [Test]
        public void ICollection() => new Collection.InRangeAttribute(1, 2).IsValid(new[] { 1, 2 }).Should().BeTrue();

        [Test]
        public void type_with_count_property() => new Collection.InRangeAttribute(3, 40).IsValid(new MyCollection(17)).Should().BeTrue();
    }

    public class Does_not_support
    {
        [Test]
        public void type_without_count_property()
            => new Collection.InRangeAttribute(1, 4).Invoking(a => a.IsValid(new object()))
            .Should().Throw<UnsupportedType>().WithMessage("Collection.InRangeAttribute does not support properties of the type object.");
    }


    public class Ingores
    {
        [Test]
        public void @null() => new Collection.InRangeAttribute(2, 4).IsValid(null).Should().BeTrue();

        [Test]
        public void length_zero() => new Collection.InRangeAttribute(2, 4).IsValid(string.Empty).Should().BeTrue();
    }


    [Test]
    public void validates([Range(4, 10)] int value) => new Collection.InRangeAttribute(4, 10).IsValid(new byte[value]).Should().BeTrue();

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    public void Invalidates(int value) => new Collection.InRangeAttribute(3, 4).IsValid(new byte[value]).Should().BeFalse();

    [TestCase("nl", "Het aantal items van veld InRangeProp moet tussen 3 en 4 zitten.")]
    [TestCase("en", "The number of items of the InRangeProp field should be between 3 and 4.")]
    public void With_message(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model() { InRangeProp = "a" }.Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "InRangeProp"));
    }
}

file sealed class Model
{
    [Collection.InRange(3, 4)]
    public string? InRangeProp { get; init; } = "abcd";

    [Collection.AtLeast(4)]
    public string? AtLeastProp { get; init; } = "abcd";

    [Collection.AtMost(4)]
    public string? AtMostProp { get; init; } = "abcd";
}

file sealed record MyCollection(long Count);
