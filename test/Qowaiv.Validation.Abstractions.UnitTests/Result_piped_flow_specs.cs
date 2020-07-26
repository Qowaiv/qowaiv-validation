using NUnit.Framework;
using Qowaiv.Validation.TestTools;
using System;

namespace Qowaiv.Validation.Abstractions.UnitTests
{
    public class Result_piped_flow_specs
    {
        [Test]
        public void X()
        {
            var pipe = new Pipe().Light()
                | (p => p.Smoke())
                | (p => p.Clean());

            var act = new Pipe().Light()
                .Act(p => p.Smoke())
                .Act(p => p.Clean());

            ValidationMessageAssert.IsValid(pipe);
            ValidationMessageAssert.IsValid(act);
            Assert.AreEqual(new Pipe(3), pipe.Value);
            Assert.AreEqual(new Pipe(3), act.Value);
        }

        public static Func<T, Result<T>> F<T>(Func<T, Result<T>> action) => action;
    }


    internal sealed class Pipe : IEquatable<Pipe>
    {
        private int i;

        public Pipe(int i = 0) => this.i = i;

        public Result<Pipe> Light() => Result.For(Next());

        public Result<Pipe> Smoke() => Result.For(Next());

        public Result Clean()
        {
            Next();
            return Result.OK;
        }

        private Pipe Next()
        {
            i++;
            return this;
        }

        public override bool Equals(object obj) => obj is Pipe other && Equals(other);
        public bool Equals(Pipe other) => other != null && i == other.i;
        public override int GetHashCode() => throw new NotSupportedException();
    }
}
