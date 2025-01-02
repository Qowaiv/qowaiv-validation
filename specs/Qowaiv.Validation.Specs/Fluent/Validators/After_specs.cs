using FluentValidation;
using Qowaiv.Validation.Fluent;

namespace Fluent_validation.After_specs;

public class Is_valid
{
    [Test]
    public void When_after()
        => new Model { DateOfBirth = new(2000, 01, 02), DateOfDeath = new Date(2000, 01, 02) }
        .Should().BeValidFor(new FixedValidator());

    [Test]
    public void When_nullable_not_set()
       => new Model { DateOfBirth = new(2000, 01, 02), DateOfDeath = null }
       .Should().BeValidFor(new FixedValidator());
}

public class Is_not_valid
{
    [TestCase("2000-01-01", "'Date Of Birth' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is niet na 01-01-2000.", "nl-NL")]
    public void When_not_after_fixed(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .Should().BeInvalidFor(new FixedValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("2000-01-01", "'Date Of Death' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is niet na 01-01-2000.", "nl-NL")]
    public void When_nullable_not_after_fixed(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .Should().BeInvalidFor(new FixedValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }

    [TestCase("2000-01-01", "'Date Of Birth' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is niet na 01-01-2000.", "nl-NL")]
    public void When_not_after_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .Should().BeInvalidFor(new ExpressionValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("2000-01-01", "'Date Of Death' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is niet na 01-01-2000.", "nl-NL")]
    public void When_nullable_not_after_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .Should().BeInvalidFor(new ExpressionValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }

    [TestCase("2000-01-01", "'Date Of Birth' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is niet na 01-01-2000.", "nl-NL")]
    public void When_not_after_nullable_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .Should().BeInvalidFor(new ExpressionNullableValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("2000-01-01", "'Date Of Death' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is niet na 01-01-2000.", "nl-NL")]
    public void When_nullable_not_after_nullable_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .Should().BeInvalidFor(new ExpressionNullableValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }
}

internal sealed class Model
{
    public Date DateOfBirth { get; init; } = new Date(2000, 01, 02);

    public Date? DateOfDeath { get; init; }
}

internal sealed class FixedValidator : ModelValidator<Model>
{
    public FixedValidator()
    {
        RuleFor(m => m.DateOfBirth).After(new Date(2000, 01, 01));
        RuleFor(m => m.DateOfDeath).After(new Date(2000, 01, 01));
    }
}

internal sealed class ExpressionValidator : ModelValidator<Model>
{
    private static readonly Date Date = new(2000, 01, 01);

    public ExpressionValidator()
    {
        RuleFor(m => m.DateOfBirth).After(_ => Date);
        RuleFor(m => m.DateOfDeath).After(_ => Date);
    }
}

internal sealed class ExpressionNullableValidator : ModelValidator<Model>
{
    private static readonly Date? Date = new Date(2000, 01, 01);
    
    public ExpressionNullableValidator()
    {
        RuleFor(m => m.DateOfBirth).After(_ => Date);
        RuleFor(m => m.DateOfDeath).After(_ => Date);
    }
}
