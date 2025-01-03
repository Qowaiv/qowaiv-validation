using FluentValidation;
using Qowaiv.Validation.Fluent;

namespace Fluent_validation.NotBefore_specs;

public class Is_valid
{
    [TestCaseSource(nameof(Validators))]
    public void When_not_before(ModelValidator<Model> validator)
        => new Model { DateOfBirth = new(2010, 11, 27), DateOfDeath = new Date(2010, 09, 18) }
        .Should().BeValidFor(validator);

    [TestCaseSource(nameof(Validators))]
    public void When_nullable_not_set(ModelValidator<Model> validator)
        => new Model { DateOfBirth = new(2010, 11, 27), DateOfDeath = null }
        .Should().BeValidFor(validator);

    static readonly IEnumerable<ModelValidator<Model>> Validators = [new FixedValidator(), new ExpressionValidator(), new ExpressionNullableValidator()];
}

public class Is_not_valid
{
    [TestCase("1983-05-02", "'Date Of Birth' is before 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is voor 01-01-2000.", "nl-NL")]
    public void When_before_fixed(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .Should().BeInvalidFor(new FixedValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("1983-05-02", "'Date Of Death' is before 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is voor 01-01-2000.", "nl-NL")]
    public void When_nullable_before_fixed(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .Should().BeInvalidFor(new FixedValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }

    [TestCase("1983-05-02", "'Date Of Birth' is before 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is voor 01-01-2000.", "nl-NL")]
    public void When_before_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .Should().BeInvalidFor(new ExpressionValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("1983-05-02", "'Date Of Death' is before 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is voor 01-01-2000.", "nl-NL")]
    public void When_nullable_before_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .Should().BeInvalidFor(new ExpressionValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }

    [TestCase("1983-05-02", "'Date Of Birth' is before 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Birth' is voor 01-01-2000.", "nl-NL")]
    public void When_before_nullable_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfBirth = dt }
            .Should().BeInvalidFor(new ExpressionNullableValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfBirth"));
    }

    [TestCase("1983-05-02", "'Date Of Death' is before 01/01/2000.", "en-GB")]
    [TestCase("1999-12-31", "'Date Of Death' is voor 01-01-2000.", "nl-NL")]
    public void When_nullable_before_nullable_expression(Date dt, string message, CultureInfo culture)
    {
        using var _ = culture.Scoped();

        new Model { DateOfDeath = dt }
            .Should().BeInvalidFor(new ExpressionNullableValidator())
            .WithMessage(ValidationMessage.Error(message, "DateOfDeath"));
    }
}

public sealed class Model
{
    public Date DateOfBirth { get; init; } = new(2000, 01, 02);

    public Date? DateOfDeath { get; init; }
}

internal sealed class FixedValidator : ModelValidator<Model>
{
    public FixedValidator()
    {
        RuleFor(m => m.DateOfBirth).NotBefore(new Date(2000, 01, 01));
        RuleFor(m => m.DateOfDeath).NotBefore(new Date(2000, 01, 01));
    }
}

internal sealed class ExpressionValidator : ModelValidator<Model>
{
    private static readonly Date Date = new(2000, 01, 01);

    public ExpressionValidator()
    {
        RuleFor(m => m.DateOfBirth).NotBefore(_ => Date);
        RuleFor(m => m.DateOfDeath).NotBefore(_ => Date);
    }
}

internal sealed class ExpressionNullableValidator : ModelValidator<Model>
{
    private static readonly Date? Date = new Date(2000, 01, 01);
    
    public ExpressionNullableValidator()
    {
        RuleFor(m => m.DateOfBirth).NotBefore(_ => Date);
        RuleFor(m => m.DateOfDeath).NotBefore(_ => Date);
    }
}
