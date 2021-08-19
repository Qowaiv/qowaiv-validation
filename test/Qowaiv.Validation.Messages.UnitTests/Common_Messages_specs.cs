using NUnit.Framework;
using Qowaiv.Validation.Abstractions;
using System;
using System.Security;
using NS = Qowaiv.Validation.Messages;

namespace Common_Messages_specs
{
    public class AccessDenied
    {
        [Test]
        public void Implements_IValidationMessage()
            => Assert.IsInstanceOf<IValidationMessage>(new NS.AccessDenied());

        [Test]
        public void Inherits_from_SecurityException()
            => Assert.IsInstanceOf<SecurityException>(new NS.AccessDenied());

        [Test]
        public void Has_error_validation_severity()
            => Assert.AreEqual(ValidationSeverity.Error, new NS.AccessDenied().Severity);

        [Test]
        public void Has_descriptive_default_message()
            => Assert.AreEqual("Access denied.", new NS.AccessDenied().Message);
    }

    public class ConcurrencyIssue
    {
        [Test]
        public void Implements_IValidationMessage()
            => Assert.IsInstanceOf<IValidationMessage>(new NS.ConcurrencyIssue());

        [Test]
        public void Inherits_from_InvalidOperationException()
            => Assert.IsInstanceOf<InvalidOperationException>(new NS.ConcurrencyIssue());

        [Test]
        public void Has_error_validation_severity()
            => Assert.AreEqual(ValidationSeverity.Error, new NS.ConcurrencyIssue().Severity);

        [Test]
        public void Has_descriptive_default_message()
            => Assert.AreEqual("A concurrency issues occurred.", new NS.ConcurrencyIssue().Message);

        [Test]
        public void Has_MidAirCollision_message()
            => Assert.AreEqual("A mid-air collision was detected.", NS.ConcurrencyIssue.MidAirCollision().Message);

        [Test]
        public void Has_VersionMismatch_message()
            => Assert.AreEqual("Expected version 17, but got version 666.", NS.ConcurrencyIssue.VersionMismatch(expectedVersion: 17, actualVersion: 666).Message);
    }

    public class EntityNotFound
    {
        [Test]
        public void Implements_IValidationMessage()
              => Assert.IsInstanceOf<IValidationMessage>(new NS.EntityNotFound());

        [Test]
        public void Inherits_from_InvalidOperationException()
            => Assert.IsInstanceOf<InvalidOperationException>(new NS.EntityNotFound());

        [Test]
        public void Has_error_validation_severity()
            => Assert.AreEqual(ValidationSeverity.Error, new NS.EntityNotFound().Severity);

        [Test]
        public void Has_descriptive_default_message()
            => Assert.AreEqual("Entity could not be found.", new NS.EntityNotFound().Message);

        [Test]
        public void Contains_Id_in_message_when_specified()
            => Assert.AreEqual("Entity with ID 17 could not be found.", NS.EntityNotFound.ForId(17).Message);
    }

    public class ServiceUnavailable
    {
        [Test]
        public void Implements_IValidationMessage()
             => Assert.IsInstanceOf<IValidationMessage>(new NS.ServiceUnavailable());

        [Test]
        public void Inherits_from_Exception()
            => Assert.IsInstanceOf<Exception>(new NS.ServiceUnavailable());

        [Test]
        public void Has_error_validation_severity()
            => Assert.AreEqual(ValidationSeverity.Error, new NS.ServiceUnavailable().Severity);

        [Test]
        public void Has_descriptive_default_message()
            => Assert.AreEqual("The requested service is unavailable.", new NS.ServiceUnavailable().Message);

        [Test]
        public void With_name_in_message_when_specified()
            => Assert.AreEqual("The service 'GitHubBuild' is unavailable.", NS.ServiceUnavailable.WithName("GitHubBuild").Message);
    }

}
