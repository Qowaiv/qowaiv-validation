using Qowaiv.Validation.Abstractions;
using static Specs.Abstractions.Arrange;

namespace Abstractions.Valdation_messages_specs;

public class Messages
{
    [Test]
    public void Are_sorted()
        => ValidationMessages.New([Warning1, Info1, Error1, Info2])
        .Should().BeEquivalentTo([Error1, Warning1, Info1, Info2]);

    [Test]
    public void Filters_Errors()
        => ValidationMessages.New([Warning1, Info1, Error1, Error2,Info2]).Errors
        .Should().BeEquivalentTo([Error1, Error2]);

    [Test]
    public void Filters_Warnings()
        => ValidationMessages.New([Warning1, Info1, Error1, Error2, Warning2, Info2]).Warnings
        .Should().BeEquivalentTo([Warning1, Warning2]);
    [Test]
    public void Filters_Infos()
        => ValidationMessages.New([Warning1, Info1, Error1, Error2, Warning2, Info2]).Infos
        .Should().BeEquivalentTo([Info1, Info2]);
}

public class Not_initalized_does_not_throw
{
    private readonly ValidationMessages NotInitalized = default;

    [Test]
    public void Count()
        => NotInitalized.Count.Should().Be(0);

    [Test]
    public void GetEnumerator()
        => NotInitalized.GetEnumerator().MoveNext().Should().BeFalse();

    [Test]
    public void Errors()
        => NotInitalized.Errors.Should().BeEmpty();

    [Test]
    public void Warnings()
        => NotInitalized.Warnings.Should().BeEmpty();

    [Test]
    public void Infos()
        => NotInitalized.Infos.Should().BeEmpty();
}
