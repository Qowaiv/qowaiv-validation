using Qowaiv.Validation.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Data_annotations;

internal static class Model
{
    public sealed class WithDisplay
    {
        [Length.AtMost(2)]
        [Display(Name = "Property")]
        public string? Prop { get; init; }
    }

    public sealed class WithAnnotatedProperty
    {
        [Length.AtLeast(3)]
        public string? Name { get; init; }
    }

    public sealed class WithIndexedProperty
    {
        [Required]
        public int this[int index] => index * 42;
    }

    public sealed class WithInaccessibleProperty
    {
        [Allowed<int>("42")]
        public int ThrowsOnGet => throw new NotImplementedException(ToString());
    }

    public sealed class WithoutAnnotations
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

    public sealed class WithSetOnlyProperty
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

    public sealed class WithTypeAnnotatedMember
    {
        public WithTypeAnnotation? Member { get; init; }
    }

    [InvalidClass]
    public sealed class WithTypeAnnotation { }
}


[AttributeUsage(AttributeTargets.Class)]
internal sealed class InvalidClassAttribute() : ValidationAttribute("This is an invalid class")
{
    public override bool IsValid(object? value) => false;
}
