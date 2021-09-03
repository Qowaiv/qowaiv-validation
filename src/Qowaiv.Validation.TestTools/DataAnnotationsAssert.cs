using Qowaiv.Validation.Abstractions;
using Qowaiv.Validation.DataAnnotations;
using System;
using System.Diagnostics;

namespace Qowaiv.Validation.TestTools
{
    /// <summary>Assertion helper class on data annotations.</summary>
    [Obsolete("Use Should()")]
    public static class DataAnnotationsAssert
    {
        /// <summary>Asserts that the model is valid, throws if not.</summary>
        [DebuggerStepThrough]
        public static void IsValid<T>(T model,  params IValidationMessage[] expected)
            => IsValid(model, null, expected);

        /// <summary>Asserts that the model is valid, throws if not.</summary>
        [DebuggerStepThrough]
        public static void IsValid<T>(T model, AnnotatedModelValidator<T> validator, params IValidationMessage[] expected)
        {
            var result = (validator ?? new AnnotatedModelValidator<T>()).Validate(model);
            ValidationMessageAssert.IsValid(result, expected);
        }

        /// <summary>Asserts the model to be invalid with specific messages. Throws if not.</summary>
        [DebuggerStepThrough]
        public static void WithErrors<T>(T model, params IValidationMessage[] expected)
            => WithErrors(model, null, expected);

        /// <summary>Asserts the model to be invalid with specific messages. Throws if not.</summary>
        [DebuggerStepThrough]
        public static void WithErrors<T>(T model, AnnotatedModelValidator<T> validator, params IValidationMessage[] expected)
        {
            var result = (validator ?? new AnnotatedModelValidator<T>()).Validate(model);
            ValidationMessageAssert.WithErrors(result, expected);
        }
    }
}
