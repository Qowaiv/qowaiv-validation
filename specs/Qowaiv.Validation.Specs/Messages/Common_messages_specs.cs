using Qowaiv.Validation.Abstractions;
using System.Security;
using NS = Qowaiv.Validation.Messages;

namespace Validation_messages.Common_messages_specs;

public class AccessDenied
{
    [Test]
    public void Implements_IValidationMessage()
        => typeof(NS.AccessDenied).Should().Implement<IValidationMessage>();

    [Test]
    public void Derives_from_SecurityException()
        => typeof(NS.AccessDenied).Should().BeDerivedFrom<SecurityException>();

    [Test]
    public void Has_error_validation_severity()
        => new NS.AccessDenied().Severity.Should().Be(ValidationSeverity.Error);

    [Test]
    public void Has_descriptive_default_message()
        => new NS.AccessDenied().Message.Should().Be("Access denied.");
}

public class ConcurrencyIssue
{
    [Test]
    public void Implements_IValidationMessage()
        => typeof(NS.ConcurrencyIssue).Should().Implement<IValidationMessage>();

    [Test]
    public void Derives_from_InvalidOperationException()
        => typeof(NS.ConcurrencyIssue).Should().BeDerivedFrom<InvalidOperationException>();

    [Test]
    public void Has_error_validation_severity()
        => new NS.ConcurrencyIssue().Severity.Should().Be(ValidationSeverity.Error);

    [Test]
    public void Has_descriptive_default_message()
        => new NS.ConcurrencyIssue().Message.Should().Be("A concurrency issues occurred.");

    [Test]
    public void Has_MidAirCollision_message()
        => NS.ConcurrencyIssue.MidAirCollision().Message.Should().Be("A mid-air collision was detected.");

    [Test]
    public void Has_VersionMismatch_message()
        => NS.ConcurrencyIssue.VersionMismatch(expectedVersion: 17, actualVersion: 666)
        .Message.Should().Be("Expected version 17, but got version 666.");
}

public class EntityNotFound
{
    [Test]
    public void Implements_IValidationMessage()
        => typeof(NS.EntityNotFound).Should().Implement<IValidationMessage>();

    [Test]
    public void Derives_from_InvalidOperationException()
        => typeof(NS.EntityNotFound).Should().BeDerivedFrom<InvalidOperationException>();

    [Test]
    public void Has_error_validation_severity()
        => new NS.EntityNotFound().Severity.Should().Be(ValidationSeverity.Error);

    [Test]
    public void Has_descriptive_default_message()
        => new NS.EntityNotFound().Message.Should().Be("Entity could not be found.");

    [Test]
    public void Contains_Id_in_message_when_specified()
        => Assert.AreEqual("Entity with ID 17 could not be found.", NS.EntityNotFound.ForId(17).Message);
}

public class ServiceUnavailable
{
    [Test]
    public void Implements_IValidationMessage()
        => typeof(NS.ServiceUnavailable).Should().Implement<IValidationMessage>();

    [Test]
    public void Derives_from_Exception()
        => typeof(NS.ServiceUnavailable).Should().BeDerivedFrom<Exception>();

    [Test]
    public void Has_error_validation_severity()
        => new NS.ServiceUnavailable().Severity.Should().Be(ValidationSeverity.Error);

    [Test]
    public void Has_descriptive_default_message()
        => new NS.ServiceUnavailable().Message.Should().Be("The requested service is unavailable.");

    [Test]
    public void With_name_in_message_when_specified()
        => NS.ServiceUnavailable.WithName("GitHubBuild").Message.Should().Be("The service 'GitHubBuild' is unavailable.");
}
