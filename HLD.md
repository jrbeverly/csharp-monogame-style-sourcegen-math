# High-Level Design

## Status

This document is the authoritative build document for this experiment.

It supersedes `MOCK-DESIGN.md`.

## Objective

Prove that a Roslyn incremental source generator can synthesize an ergonomic XNA/MonoGame-style vector library from declarative definitions owned by the generator, across two numeric variants, using a single mutable API shape.

## Locked Technology Choices

- Language: C#, `LangVersion` 12
- Generation model: Roslyn incremental source generators (`IIncrementalGenerator`)
- Generator target: `netstandard2.0`, consumed by the library as an analyzer
- Library and test targets: `net8.0`
- Test framework: xUnit v3
- Solution format: `.slnx`, driven by a Makefile

## Locked Type Scope

The first generated types are `Vector2` and `Vector3`.

`Vector4`, matrices, quaternions, and every geometry primitive are out of scope for this milestone.

## Locked Numeric Variants

`float` and `double`.

Type naming: the `float` variant uses the bare XNA names (`Vector2`, `Vector3`). The `double` variant uses the `d` suffix (`Vector2d`, `Vector3d`). All variants live in the `MonoGameMath` namespace; the suffix alone distinguishes the `double` variants from the bare `float` names.

`int` and `byte` are deferred. When they arrive, `byte` is treated as a storage-oriented type, not a full arithmetic backend.

## Locked Generated-Member List

Every generated type, for every numeric variant, exposes exactly this surface:

Fields:

- public mutable fields, one per component, named `X`, `Y` (and `Z` for `Vector3`)

Constructors:

- a single-value constructor (all components from one value)
- a component-wise constructor (one parameter per component)

Static predefined values:

- `Zero`
- `One`
- `UnitX`
- `UnitY`
- `UnitZ` (`Vector3` only)

Operators:

- unary `-`
- binary `+` and `-` (component-wise)
- binary `*` and `/` (component-wise vector-vector, vector-scalar, and scalar-vector)
- `==` and `!=`

Static copy-returning operations:

- `Add`, `Subtract`, `Multiply`, `Divide`
- `Negate`, `Normalize`
- `Dot`, `Cross` (`Vector3` only)
- `Length`, `LengthSquared`
- `Distance`, `DistanceSquared`
- `Lerp`, `Clamp`, `Min`, `Max`, `Reflect`

Instance in-place operations:

- `Normalize()`
- `Add(value)` and `Subtract(value)` — in-place counterparts of the static `Add`/`Subtract` that mutate the receiver

Object contract:

- `IEquatable<T>`, `Equals(object)`, `GetHashCode()`, `ToString()`

Deferred members:

- matrix-dependent members such as `Transform` and `TransformNormal` arrive with matrices
- curve-interpolation helpers (`Hermite`, `CatmullRom`, `SmoothStep`, `Barycentric`) and component rounding helpers (`Round`, `Ceiling`, `Floor`) are deferred

## Locked Mutable-API Shape

Exactly one public shape exists per type: a single mutable struct.

- fields are public and mutable
- `Normalize()`, `Add(value)`, and `Subtract(value)` mutate in place, matching XNA
- every other operation is static and returns a copy
- there are no immutable variants, read-only wrappers, or `ref`/`out`-based overloads

The API lineage is XNA as maintained by MonoGame. Where the two diverge, MonoGame is the reference.

## Locked Declarative-Definition Form

Definitions are project-owned declarative C# record data inside the generator project.

A definition declares the generated type name, the ordered component names, and the numeric kind.

The member list and mutability policy are fixed generator policy, not per-definition data.

There is no separate definition layer in this sliver: definition data is already typed C#, so no parser, importer, DSL, or normalization step exists. External formats and adapters are deferred.

## Resolved Open Decisions

These resolve the open decisions raised in `MOCK-DESIGN.md`.

| Open decision | Resolution |
|---|---|
| Canonical authoring model | Project-owned definitions in the generator; external formats deferred |
| Primary public API style | XNA/MonoGame mutable-struct lineage; MonoGame is the tiebreaker |
| First-class arithmetic numeric types | `float` and `double`; `int` and `byte` deferred |
| Number of public mutability models | Exactly one: the mutable struct |
| Geometry conventions | Deferred with geometry; vector-only conventions locked below |
| Where generated code lives | Emitted at compile time into the referencing compilation |

## Vector-Only Conventions

The only conventions locked in this sliver:

- component order is `X`, `Y`, `Z`
- `Cross` follows the XNA right-handed convention

All other conventions (handedness, matrix layout, multiplication order, angle units, boundary rules) are geometry concerns and remain open.

## Determinism Contract

Generated floating-point behavior is performance-oriented best effort.

Results are not guaranteed to be bit-identical across platforms or runtimes.

## System Boundaries

**Generator** (`MonoGameMath.Generator`): owns the definitions, the fixed member policy, and the emission logic. Referenced by the consumer as an analyzer with `ReferenceOutputAssembly=false`.

**Consumer** (`MonoGameMath.Vectors`): the generated runtime library. Contains no handwritten types; every public type is emitted by the generator into this compilation.

**Tests** (`MonoGameMath.Vectors.Tests`): xUnit v3 project referencing the consumer. Contains no tests in this issue.

## Explicit Deferrals

- `Vector4`, matrices, quaternions
- geometry primitives (rectangles, triangles, rays, planes, bounding boxes, circles, spheres) and collision or intersection operations
- `int`, `byte`, and other numeric variants
- immutable variants, read-only wrappers, `ref`-based operations, functional-style APIs
- external definition formats, DSLs, importers, and symbolic systems
- matrix-dependent members (`Transform`, `TransformNormal`)
- curve-interpolation and rounding helper members
- geometry conventions (handedness, matrix layout, angle units, boundary rules)
- SIMD-specific optimization beyond SIMD-compatible layouts
- packaging, NuGet, and downstream-consumer workflows
- benchmarks

## Implementation Sequence

1. Scaffold the solution, Makefile, generator stub, empty consumer, and empty test project (this issue).
2. Implement the definition model and the fixed member policy inside the generator.
3. Emit `Vector2` and `Vector3` for `float`.
4. Emit the `double` variants.
5. Validate end-to-end generation against the consumer.
6. Add xUnit v3 tests for the generated surface.
