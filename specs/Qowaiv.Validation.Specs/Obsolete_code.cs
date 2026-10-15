namespace Obsolete_code;

[Obsolete("Will be dropped when the next major version is released.")]
public class Will_be_dropped
{
    [Test]
    public async Task Result_AsTask()
        => (await Qowaiv.Validation.Abstractions.Result.For(42).AsTask())
        .Should()
        .BeValid()
        .WithMessages()
        .Value.Should().Be(42);

    [Test]
    public void double_Percent_humanizer()
        => 12.00.Percent().Should().NotBe(0.Percent());
}

[Obsolete("Will become private when the next major version is released.")]
public class Will_become_private;
