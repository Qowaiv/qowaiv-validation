using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using RangeAttribute = NUnit.Framework.RangeAttribute;

namespace Data_annotations.Attributes.Length_specs;

public class At_least
{
    public class Supports
    {
        [Test]
        public void @string() => new Length.AtLeastAttribute(4).IsValid("1234").Should().BeTrue();

        [Test]
        public void @array() => new Length.AtLeastAttribute(2).IsValid(new int[] { 1, 2 }).Should().BeTrue();

        [Test]
        public void type_with_length_property() => new Length.AtLeastAttribute(4).IsValid(EmailAddress.Parse("test@qowaiv.org")).Should().BeTrue();
    }

    public class Does_not_support
    {
        [Test]
        public void type_without_length_property()
            => new Length.AtLeastAttribute(4).Invoking(a => a.IsValid(new List<int>()))
            .Should().Throw<UnsupportedType>().WithMessage("Length.AtLeastAttribute does not support properties of the type List<int>.");
    }

    public class Ingores
    {
        [Test]
        public void @null() => new Length.AtLeastAttribute(0).IsValid(null).Should().BeTrue();

        [Test]
        public void length_zero() => new Length.AtLeastAttribute(4).IsValid(string.Empty).Should().BeTrue();
    }

    [Test]
    public void validates([Range(4, 10)] int value) => new Length.AtLeastAttribute(4).IsValid(new byte[value]).Should().BeTrue();

    [Test]
    public void Invalidates([Range(1, 10)] int value) => new Length.AtLeastAttribute(11).IsValid(new byte[value]).Should().BeFalse();

    [TestCase("nl", "De lengte van het veld AtLeastProp moet minstens 4 zijn.")]
    [TestCase("en", "The length of the AtLeastProp field should be at least 4.")]
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
        public void @string() => new Length.AtMostAttribute(4).IsValid("1234").Should().BeTrue();

        [Test]
        public void @array() => new Length.AtMostAttribute(2).IsValid(new int[] { 1, 2 }).Should().BeTrue();

        [Test]
        public void type_with_length_property() => new Length.AtMostAttribute(40).IsValid(EmailAddress.Parse("test@qowaiv.org")).Should().BeTrue();
    }

    public class Does_not_support
    {
        [Test]
        public void type_without_length_property()
            => new Length.AtMostAttribute(4).Invoking(a => a.IsValid(new List<int>()))
            .Should().Throw<UnsupportedType>().WithMessage("Length.AtMostAttribute does not support properties of the type List<int>.");
    }

    public class Ingores
    {
        [Test]
        public void @null() => new Length.AtMostAttribute(0).IsValid(null).Should().BeTrue();
    }

    [Test]
    public void validates([Range(1, 4)] int value) => new Length.AtMostAttribute(4).IsValid(new byte[value]).Should().BeTrue();

    [Test]
    public void Invalidates([Range(12, 20)] int value) => new Length.AtMostAttribute(11).IsValid(new byte[value]).Should().BeFalse();

    [TestCase("nl", "De lengte van het veld AtMostProp mag niet meer dan 4 zijn.")]
    [TestCase("en", "The length of the AtMostProp field should be at most 4.")]
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
        public void @string() => new Length.InRangeAttribute(1,4).IsValid("1234").Should().BeTrue();

        [Test]
        public void @array() => new Length.InRangeAttribute(1, 2).IsValid(new int[] { 1, 2 }).Should().BeTrue();

        [Test]
        public void type_with_length_property() => new Length.InRangeAttribute(3, 40).IsValid(EmailAddress.Parse("test@qowaiv.org")).Should().BeTrue();
    }

    public class Does_not_support
    {
        [Test]
        public void type_without_length_property()
            => new Length.InRangeAttribute(1, 4).Invoking(a => a.IsValid(new List<int>()))
            .Should().Throw<UnsupportedType>().WithMessage("Length.InRangeAttribute does not support properties of the type List<int>.");
    }


    public class Ingores
    {
        [Test]
        public void @null() => new Length.InRangeAttribute(2, 4).IsValid(null).Should().BeTrue();

        [Test]
        public void length_zero() => new Length.InRangeAttribute(2, 4).IsValid(string.Empty).Should().BeTrue();
    }


    [Test]
    public void validates([Range(4, 10)] int value) => new Length.InRangeAttribute(4, 10).IsValid(new byte[value]).Should().BeTrue();

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    public void Invalidates(int value) => new Length.InRangeAttribute(3, 4).IsValid(new byte[value]).Should().BeFalse();

    [TestCase("nl", "De lengte van het veld 3 moet tussen de 3 en 4 zijn.")]
    [TestCase("en", "The length of the InRangeProp field should be between 3 and 4.")]
    public void With_message(CultureInfo culture, string message)
    {
        using var _ = culture.Scoped();
        new Model() { InRangeProp = "a" }.Should().BeInvalidFor(new AnnotatedModelValidator<Model>())
            .WithMessage(ValidationMessage.Error(message, "InRangeProp"));
    }
}

file sealed class Model
{
    [Length(0, 4)]
    public string? Does_not_conflict_with_System_ComponentModel_DataAnnotations { get; init; }

    [Length.InRange(3, 4)]
    public string? InRangeProp { get; init; } = "abcd";

    [Length.AtLeast(4)]
    public string? AtLeastProp { get; init; } = "abcd";

    [Length.AtMost(4)]
    public string? AtMostProp { get; init; } = "abcd";
}
