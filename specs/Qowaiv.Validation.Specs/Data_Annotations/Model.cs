using Qowaiv.Diagnostics.Contracts;
using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Data_annotations;

internal static class Model
{
    public static class ClockDependent
    {
        public sealed class InFuture
        {
            [InFuture]
            public DateOnly? ExpiryDate { get; init; }
        }

        public sealed class InPast
        {
            [InPast]
            public Year YearOfConstruction { get; init; }
        }

        public sealed class NotInFuture
        {
            [NotInFuture]
            public DateOnly DateOfBirth { get; init; }
        }

        public sealed class NotInPast
        {
            [NotInPast]
            public DateOnly ExpiryDate { get; init; }
        }
    }
    public static class With
    {
        public sealed class AnnotatedProperty
        {
            [Length.AtLeast(3)]
            public string? Name { get; init; }
        }

        public sealed class Display
        {
            [Length.AtMost(2)]
            [Display(Name = "Property")]
            public string? Prop { get; init; }
        }

        public sealed class InaccessibleProperty
        {
            [Allowed<int>("42")]
            public int ThrowsOnGet => throw new NotImplementedException(ToString());
        }

        public sealed class IndexedProperty
        {
            [Required]
            public int this[int index] => index * 42;
        }

        public sealed class InheritableMember
        {
            public Inheritable? Member { get; init; }
        }

        public sealed class RequiredOnValueType
        {
            [Required]
            public int? Required { get; init; }

            [Required]
            public bool Ignored { get; init; }

            [Mandatory]
            public EmailAddress Mandatory { get; init; } 
        }

        public sealed class SetOnlyProperty
        {
#pragma warning disable S2376 // Write-only properties should not be used
            // This is a test to check if write-only properties are handled correctly.
            [Mandatory]
            public int SomeProperty
            {
                set => field = value;
            }
#pragma warning restore S2376
        }
    }

    public static class Without
    {
        public sealed class Annotations
        {
            public FileInfo? File { get; init; }

            public string? Name { get; init; }

            public int Number { get; init; }

            public DateTime CreatedUtc => File?.CreationTimeUtc ?? Clock.UtcNow();

            [SkipValidation]
            public required string Required { get; init; }

            public Parent? WithLoop { get; init; }

            public sealed class Parent
            {
                public Child[] Childen { get; init; } = [];
            }

            public sealed class Child
            {
                public Parent? Parent { get; init; }
            }
        }
    }

    [Inheritable]
    public class Inheritable { }

    public class Inherited : Inheritable
    {
        [Allowed<int>("42")]
        public int Value { get; init; }
    }
}
