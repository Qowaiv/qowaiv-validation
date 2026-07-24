# AGENTS.md

## What this is

Multi-package .NET class library (NuGet packages) for domain model validation. Wraps DataAnnotations and FluentValidation behind a common `IValidator<TModel>` / `Result<TModel>` abstraction. C# 14.0, nullable enabled.

## Commands

```bash
# Build (also runs all Roslyn analyzers — this IS the lint step)
dotnet build qowaiv-validation.slnx -c Release

# Test (CI filter excludes Generators category)
dotnet test qowaiv-validation.slnx -c Release --no-build --filter TestCategory!=Generators

# Pack
dotnet pack qowaiv-validation.slnx -v normal --no-build --no-restore -c Release --output packages
```

There is no separate lint or typecheck command. `EnforceCodeStyleInBuild=true` in `Directory.Build.props` means the build itself enforces all analyzer rules (SonarAnalyzer, StyleCop, NUnit analyzers, Qowaiv analyzers, AwesomeAssertions analyzers).

## Solution and project structure

- **Solution file:** `qowaiv-validation.slnx` (XML-based `.slnx` format, not `.sln`)
- **Source packages** (`src/`): Abstractions, DataAnnotations, Fluent, Guarding, Messages, TestTools, Xml
- **Test/benchmark projects** (`specs/`): Qowaiv.Validation.Specs (NUnit), Qowaiv.Validation.TestData, Benchmarks
- **Root `.net.csproj`** is for IDE tooling only — do not modify
- Empty directories `src/MiniValidation/` and `src/Tracing/` exist but contain no files

## Multi-targeting

Each source package targets different frameworks. Most: `netstandard2.0;net8.0;net10.0`. Fluent and TestTools: `net8.0;net10.0` only (no netstandard2.0).

## Dependency management

**Central Package Management (CPM):** All dependency versions are in `Directory.Packages.props`. Do not add `<PackageVersion>` elements to individual `.csproj` files.

**Lock files:** Every project has `packages.lock.json` (enabled globally). Locally you can run `dotnet restore` freely. In CI, `RestoreLockedMode=true` — lock files must be committed and up to date. If you add/remove a dependency, regenerate lock files before committing.

## Test conventions

- Test project: `specs/Qowaiv.Validation.Specs/`
- Test file naming: `*_specs.cs` (not `*_tests.cs`)
- Test class naming: descriptive names like `Valid_result`, `Invalid_result` (not `ResultTests`)
- Test framework: NUnit 4.x with `Assert.That` pattern (enforced as error: `NUnit2005`, `NUnit2033`)
- Assertions: AwesomeAssertions `.Should()` (enforced as error: `FAA0004`)
- Do not use `Assert.AreEqual`, `Assert.IsTrue`, etc. — analyzer will error
- Do not use NUnit `Constraint` assertions for things AwesomeAssertions covers — analyzer will error

## Key analyzer rules to know

- `S3776` = warning: Cognitive complexity limit
- `S3900` = warning: Public method args must be null-checked
- `QW0003` = warning: Pure functions must be decorated with `[Pure]`
- Many SA-series rules are disabled (`none`) — do not re-enable

Full severity map: `.globalconfig` (201 lines of diagnostic overrides).

## Shared code

`shared/Guard.cs` and `shared/ProductInfo.cs` are compiled into every source project via MSBuild `<Compile Include>` with `Link`. They are not standalone projects. Edit them in `shared/`, not in individual `src/` directories.

## Publishing

CI triggers on pushes to `main` and version tags (`v*`). Tag push produces `.nupkg` artifacts and publishes to NuGet.org. Each source package embeds its own `<Version>` in its `.csproj`. Update the version in the `.csproj` before tagging.

## Package validation

`EnablePackageValidation=true` with strict baseline validation is set in `props/package.props`. Each package declares a `PackageBaselineVersion`. Breaking API changes require updating `CompatibilitySuppressions.xml` in the relevant project.

## Gotchas

* **Do not modify `.net.csproj`:** This file is exclusively for running Roslyn Analyzers. Agents must completely ignore and skip this file during development.
