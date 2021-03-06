﻿using System;

namespace Qowaiv.Validation.DataAnnotations.UnitTests.Models
{
    public sealed class ModelWithOneState : IEquatable<ModelWithOneState>
    {
        public override bool Equals(object obj) => Equals(obj as ModelWithOneState);

        public bool Equals(ModelWithOneState other) => !(other is null);

        public override int GetHashCode() => 17;
    }
}
