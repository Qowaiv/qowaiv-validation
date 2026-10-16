# Codebase Analysis — Qowaiv.Validation

Date: 2026-10-16
Scope: `src/Qowaiv.Validation.*` (all packages) and their tests.
Objectives: (1) potential bugs, (2) performance issues, (3) spelling/grammar in documentation and comments.

Method: manual source review. Each finding below was read in the source and is referenced by
`file:line`. Findings are grouped by objective and roughly ordered by impact.

> Note: The `AsyncResult` area was being actively refactored while this review ran
> (files changed between reads). Findings that touch `AsyncResult` are called out
> explicitly and may already be in flux. All other sections target stable code.

---

## 1. Potential bugs

### 1.1 `Result.WithMessages<T>(...)` throws `NoValue` for reference types — HIGH
`src/Qowaiv.Validation.Abstractions/Result.Factories.cs:48-54`

```csharp
public static Result<T> WithMessages<T>(params IValidationMessage[] messages)
    => new Result<T>(default, FixedMessages.New(messages)).NotNull();
```

`NotNull()` (`Result_TModel.cs:326-327`) throws when `IsValid && Value is null`.
A warning/info message is **not** an error, so `IsValid` is `true` while the value is
`default(T)`. For a reference type that is `null` → `NoValue` is thrown.

Consequences:
- `Result.WithMessages<SomeClass>(ValidationMessage.Warn("..."))` throws.
- `Result.WithMessages<SomeClass>(ValidationMessage.Info("..."))` throws.
- `Result.WithMessages<SomeClass>()` (no messages) throws.
- For value types it silently succeeds with a **bogus** value (`default`, e.g. `0`), which is
  also wrong, just in a different way.

The only spec coverage is `Result.WithMessages<int>(Warning1, Info1, Info2)`
(`specs/.../Abstractions/Result_specs.cs:12`) — a value type — and the no-value throw cases for
`<object>` are asserted (`Result_specs.cs:66-75`), so the reference-type+warning case is untested.

The README example `Result.WithMessages<DataType>(ValidationMessage.Warn("Check"))` would throw
whenever `DataType` is a reference type.

### 1.2 `Result.Async<T>` overload is inconsistent and can't take a single message — LOW
`src/Qowaiv.Validation.Abstractions/Result.Factories.cs:10`

```csharp
public static AsyncResult<T> Async<T>(T value, params IEnumerable<IValidationMessage> messages)
```

`params` over `IEnumerable<IValidationMessage>` does not allow `Result.Async(17, ValidationMessage.Warn("x"))`
(no implicit conversion from `IValidationMessage` to `IEnumerable<IValidationMessage>`), unlike
`For<T>` which offers both `params IValidationMessage[]` and `IEnumerable<...>` overloads. It also
allocates an array for the common case.

### 1.3 `AsyncResult` "null task result" fallbacks are inconsistent — MEDIUM
`src/Qowaiv.Validation.Abstractions/AsyncResult_TModel.Act.cs`

The `... is { } result ? ... : ...` fallback branches use two different factories:

- `Result.Null<TOut>() / Result.Null<TModel>()` at lines 60, 87, 114 (safe), versus
- `Result.WithMessages<TModel>()` at lines 148, 182, 216, 250.

The latter throws `NoValue` for reference types if the awaited task ever yields a `null` result
(see 1.1). Either use `Result.Null<TModel>()` consistently or make the fallbacks non-throwing.

### 1.4 `SignAttribute` throws on `NaN` — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/Attributes/SignAttribute.cs:21-23`

```csharp
float v => Is(Math.Sign(v)),
double v => Is(Math.Sign(v)),
```

`Math.Sign(float.NaN)` / `Math.Sign(double.NaN)` throw `ArithmeticException`. Validating a `NaN`
against any sign attribute (`Positive`, `Negative`, `NotPositive`, `NotNegative`) throws instead of
failing validation. `MultipleOfAttribute` already guards finiteness
(`MultipleOfAttribute.cs:51-60`); `SignAttribute` should too.

### 1.5 `SetOfValuesAttribute.IsValid` can leak conversion exceptions — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/Attributes/SetOfValuesAttribute.cs:23-24`

```csharp
var converter = TypeDescriptor.GetConverter(value.GetType());
return OnEqual == Values.Exists(val => value.Equals(converter.ConvertFromInvariantString(val)));
```

`ConvertFromInvariantString` throws (`FormatException`/`NotSupportedException`) when a configured
value cannot be converted to the target type. That exception escapes validation instead of yielding
an invalid result. Wrap conversion and treat failures as non-matches.

### 1.6 `ItemsAttribute<TValidator>` ctor resolution can throw — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/Attributes/ItemsAttributes.cs:92`

```csharp
var ctor = typeof(TValidator).GetConstructors().Single(c => c.GetParameters().Length > 0);
```

`Single` throws `InvalidOperationException` when the wrapped validator type has zero parameterized
constructors (with non-empty `args`) or more than one. The error is then re-wrapped as an
`ArgumentException` with a message that has no detail.

### 1.7 `ItemsAttribute<TValidator>` uses shared mutable attribute state — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/Attributes/ItemsAttributes.cs:25,42,59-79`

`FormatErrorMessage` lazily copies `ErrorMessage*` onto the shared `Validator` instance and flips a
mutable `Init` flag; `IsValid(item)` calls the parameterless `IsValid(object)` overload, ignoring
`ValidationContext`. Validation attributes are cached and reused across threads, so concurrent
validations can race on `Init`/`Validator.ErrorMessage` and leak one attribute's message into
another. It also bypasses validators that need a context (despite `RequiresValidationContext => true`).

### 1.8 Shared static mutable `ValidationContext` — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/ValidateContext.cs:64-70` and `Validate.cs:59-60`

`ValidateContext.None` is a single static `ValidationContext` reused for every context-less
validation, but `Validate.Member` still writes to it:

```csharp
context.MemberName = member.Name;
context.DisplayName = member.Display?.GetName() ?? member.Name;
```

Concurrent validations share and mutate the same object → non-deterministic `DisplayName`/
`MemberName`. A fresh context (or a non-shared one) should be used.

### 1.9 `InvalidModelException.For(IEnumerable)` blindly trims the last character — LOW
`src/Qowaiv.Validation.Abstractions/InvalidModelException.cs:52` (and `:35`)

```csharp
sb.Append(QowaivValidationMessages.InvalidOperationException);
...
sb.Remove(sb.Length - 1, 1).AppendLine(":");
```

This assumes the resource string ends with exactly one removable terminator. If the resource were
empty, `Remove(-1, 1)` throws `ArgumentOutOfRangeException`.

### 1.10 `MemberNames` default instance throws — LOW
`src/Qowaiv.Validation.DataAnnotations/MemberNames.cs:22`

`Members` is a `readonly` field assigned only in the constructor; `default(MemberNames)`
has a `null` `Members`, so `Count`/`AddIndex`/`GetEnumerator` throw `NullReferenceException`.
Internal-only, but the struct invites misuse.

### 1.11 `PostalCodeValidation.ValidFor(Func<...>)` does not null-guard `country` — LOW
`src/Qowaiv.Validation.Fluent/FluentValidation/PostalCodeValidation.cs:32-35`

Only `ruleBuilder` is guarded; `country(model)` will `NullReferenceException` if the delegate is null.

### Checked and dismissed
- `Validate.Generalizer`… `Validate.cs:21` `enumerable.GetType().GetEnumerableType()!` is guarded by
  `annotations.CheckEnumerable`, which is only set for generic `IEnumerable<>` types, so the
  null-forgiving is currently safe (fragile, but not a live bug).
- `ValidationMessageCompare.CompareByInterface` hash and equality use the same three fields and are
  consistent.

---

## 2. Performance issues

### 2.1 Reflection `MethodInfo.Invoke` on the validation hot path — MEDIUM/HIGH
`src/Qowaiv.Validation.DataAnnotations/Extensions/System.ComponentModel.DataAnnotations.ValidationAttribute.cs:17`

```csharp
var result = NonPublic.ValidationAttribute.IsValid.Invoke(attribute, [value, validationContext]);
```

Every attribute on every member does a reflective invoke: allocates an `object[]`, boxes the value,
and goes through the reflection binder. This is the single hottest path in DataAnnotations validation.

### 2.2 `TypeAnnotations.Get` allocates a `HashSet<Type>` on every call — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/Annotations/TypeAnnotations.cs:25-26`

```csharp
internal static TypeAnnotations? Get(Type type) => Store.Get(Guard.NotNull(type), []);
```

`[]` targets `HashSet<Type>` and is allocated even when the type is already cached (the common case).
`Get` is called several times per member in `Validate.Model`.

### 2.3 `FixedMessages` cons-list gives O(n) index / O(n²) iteration — MEDIUM
`src/Qowaiv.Validation.Abstractions/Internals/FixedMessages.cs:37-45`,
`src/Qowaiv.Validation.Abstractions/Internals/SomeFixedMessages.cs:10-13`

- Indexer: `this.Skip(index).FirstOrDefault()` → O(index) per element → O(n²) indexed iteration.
- `SomeFixedMessages.Count => parent.Count + 1` is recomputed O(n) each access.
- `GetEnumerator()` materializes the whole chain with `.Reverse()` (buffers all items).
- `AddRange` builds a new node per message.

Fine as a cheap-merge persistent list, but it advertises `IReadOnlyList<T>` and any index-based
consumer degrades quadratically.

### 2.4 `ObjectWrapper` reflection and boxing — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/Reflection/ObjectWrapper.cs:21,26`

- `Try` calls `value.GetType().GetRuntimeProperty(name)` on every call (no cache).
- `TryCount` falls back to `(obj as IEnumerable)?.Cast<object>().Count()`, enumerating the whole
  sequence and boxing every element.

### 2.5 `Member` reflection without cached delegates — MEDIUM
`src/Qowaiv.Validation.DataAnnotations/Reflection/Member.cs:33-43,69,89`

- `GetValue` uses `PropertyInfo.GetValue`/`FieldInfo.GetValue` per validation.
- `GetRequiredMemberAttribute` calls `GetCustomAttributes()` a second time (after `AnnotationStore`
  already enumerated them) and uses two `Any` + `FullName` string comparisons.

### 2.6 `Result.IsValid` re-enumerates lazily — MEDIUM
`src/Qowaiv.Validation.Abstractions/Result.cs:20,38`

`IsValid => !Errors.Any()` and `Errors => Messages.GetErrors()` (LINQ `Where`) allocate an iterator
on every call. `IsValid` is evaluated repeatedly (`Value`, `NotNull`, `ThrowIfInvalid`, `Act`).
A precomputed/count-based check would avoid the allocation.

### 2.7 `XDocument.Validate(Stream / XmlSchema)` recompiles the schema each call — MEDIUM
`src/Qowaiv.Validation.Xml/Extensions/System.Xml.Linq.XObject.cs:10-16`

Both overloads build a new `XmlSchemaSet` (and therefore compile the schema) per call.
`SchemaValidator` correctly holds a reusable `XmlSchemaSet`, so only the extension overloads suffer.

### 2.8 `AbsoluteXPath` is O(depth²) — MEDIUM
`src/Qowaiv.Validation.Xml/Extensions/System.Xml.Linq.XObject.cs:46-70`

`RelativePath` runs `element.AncestorsAndSelf().SelectMany(Attributes).FirstOrDefault(...)` for each
ancestor, so building a path is quadratic in tree depth (plus LINQ allocations). Called for every
XML validation message location.

### 2.9 `NotEmptyOrUnknownValidator` reflects per validation — MEDIUM
`src/Qowaiv.Validation.Fluent/FluentValidation/NotEmptyOrUnknownValidator.cs:16`

`Unknown.Value(typeof(TProperty))` is resolved via reflection and compared with `object.Equals`
(boxing) on every call. The unknown singleton could be cached per `TProperty`.

### 2.10 `InvalidModelException.For` enumerates the message filter multiple times — LOW
`src/Qowaiv.Validation.Abstractions/InvalidModelException.cs:31-41` — `Filter` is lazily enumerated
by `Any()`, then again by `foreach`, then again inside the constructor.

### 2.11 `AnnotationStore` scans all Qowaiv value types at startup — LOW
`src/Qowaiv.Validation.DataAnnotations/Annotations/AnnotationStore.cs:27`

The constructor enumerates `typeof(Date).Assembly.GetExportedTypes()` and pre-populates the cache.
One-time cost, but non-trivial and paid per `AnnotationStore` instantiation (one static per
load context).

---

## 3. Spelling, grammar & naming

### 3.1 Public API typo: `GetSevice<T>` → `GetService<T>` — HIGH (public surface)
`src/Qowaiv.Validation.DataAnnotations/Extensions/System.ComponentModel.DataAnnotations.ValidationContextExtensions.cs:8`

Misspelled public extension method. Note it is already captured in the API-compatibility baseline
(`src/Qowaiv.Validation.DataAnnotations/CompatibilitySuppressions.xml:665,671`), so renaming is a
breaking change that requires updating that file.

### 3.2 Comment typos
| File:line | Current | Should be |
| --- | --- | --- |
| `Abstractions/IValidationMessage.cs:3` | "validation messsage" | "validation message" |
| `Abstractions/AsyncResult.cs:23` | "Implictly casts" | "Implicitly casts" |
| `Abstractions/AsyncResult_TModel.cs:26` | "Implictly casts" | "Implicitly casts" |
| `DataAnnotations/MemberPath.cs:7` | "not threat-safe" | "not thread-safe" |
| `DataAnnotations/ReferenceComparer.cs:5` | "cut of in the nested validation" | "cut off in the nested validation" |
| `DataAnnotations/UnsupportedType.cs:3` | "The exception that is thrown is a type is not supported." | "...thrown if a type is not supported." |

### 3.3 Grammar / message wording
- `DataAnnotations/Extensions/System.ComponentModel.DataAnnotations.ValidationAttribute.cs:10` —
  "The original function messes up with `ValidationMessage.None`" → e.g. "The original function does
  not handle `ValidationMessage.None` correctly".
- `DataAnnotations/Attributes/SetOfValuesAttribute.cs:16` — "Returns true if the value occurs to be
  forbidden, otherwise false." → "Returns true if the value is forbidden, otherwise false."
- `DataAnnotations/Attributes/ItemsAttributes.cs:112` — `"The args could not be used "` has a
  trailing space and no closing punctuation; also expose the inner reason.

### 3.4 File naming
`src/Qowaiv.Validation.TestTools/Assertions/ResultValidnessAssertions .cs` — filename contains a space
before the extension (`ResultValidnessAssertions .cs`); likely intended to be
`ResultValidnessAssertions.cs`.

---

## 4. Notes / follow-ups
- Fixing 3.1 (rename `GetSevice`) requires updating `CompatibilitySuppressions.xml`.
- 2.1 and 1.4 are the highest-value correctness/perf items in DataAnnotations.
- 1.1 affects the documented API (README) and should have a spec added for reference types with
  warning-only messages.
