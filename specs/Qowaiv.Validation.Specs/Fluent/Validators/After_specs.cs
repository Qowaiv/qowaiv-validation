using FluentValidation;
using Qowaiv.Validation.Fluent;

namespace Fluent_validation.After_specs;

public class Is_valid
{
    [TestCaseSource(nameof(Validators))]
    public void When_after(ModelValidator<Model> validator)
        => new Model { DateOfBirth = new(2000, 01, 02), DateOfDeath = new Date(2000, 01, 02) }
        .ShouldBeValidFor(validator);

    [TestCaseSource(nameof(Validators))]
    public void When_nullable_not_set(ModelValidator<Model> validator)
       => new Model { DateOfBirth = new(2000, 01, 02), DateOfDeath = null }
       .ShouldBeValidFor(validator);

    static readonly IEnumerable<ModelValidator<Model>> Validators = [new FixedValidator(), new ExpressionValidator(), new ExpressionNullableValidator()];
}

public class Is_not_valid
{
    [TestCase("2000-01-01", "'Date Of Birth' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is niet na 01-01-2000.", "nl-NL")]
    public void When_not_after_fixed(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .ShouldBeInvalidFor(new FixedValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("2000-01-01", "'Date Of Death' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is niet na 01-01-2000.", "nl-NL")]
    public void When_nullable_not_after_fixed(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .ShouldBeInvalidFor(new FixedValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }

    [TestCase("2000-01-01", "'Date Of Birth' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is niet na 01-01-2000.", "nl-NL")]
    public void When_not_after_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .ShouldBeInvalidFor(new ExpressionValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("2000-01-01", "'Date Of Death' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is niet na 01-01-2000.", "nl-NL")]
    public void When_nullable_not_after_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .ShouldBeInvalidFor(new ExpressionValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }

    [TestCase("2000-01-01", "'Date Of Birth' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is niet na 01-01-2000.", "nl-NL")]
    public void When_not_after_nullable_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .ShouldBeInvalidFor(new ExpressionNullableValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("2000-01-01", "'Date Of Death' is not after 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is niet na 01-01-2000.", "nl-NL")]
    public void When_nullable_not_after_nullable_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .ShouldBeInvalidFor(new ExpressionNullableValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }
}

public sealed class Model
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
