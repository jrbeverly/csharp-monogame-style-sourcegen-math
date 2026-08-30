# Technical

## Language and Platform

The implementation should be built in C#.

The architecture should heavily leverage C# source generators.

## Historical and Conceptual Inspiration

The system is inspired by:

- XNA
- MonoGame
- `System.Numerics`
- MonoGame extensions
- Numerical and geometry libraries

Particular emphasis is placed on the API ergonomics and mathematical style present in XNA-era frameworks.

## Core Mathematical Types

The generated system should support types including:

- `Vector2`
- `Vector3`
- `Vector4`
- Matrices
- Quaternions
- Rectangles
- Triangles
- Rays
- Bounding boxes
- Planes
- Circles
- Spheres
- Additional geometric primitives

## Numeric Type Variants

The architecture should support multiple numeric implementations, including:

- `int`
- `float`
- `double`
- `byte`
- Potentially additional numeric types

The generator should systematically synthesize implementations across numeric variants.

## External Mathematical Definitions

Mathematical equations and constructs should ideally be defined externally from the runtime C# implementation.

Potential approaches include:

- Open mathematical specifications
- Existing equation representation formats
- Symbolic math systems
- Math DSLs
- Existing parser ecosystems
- Structured mathematical definition files

The implementation should prefer existing ecosystems or specifications where practical.

## Source Generator Responsibilities

The source generators should synthesize:

- Mathematical types
- Operators
- Overloads
- Numeric variants
- Geometric logic
- Conversion logic
- Constructors
- Static predefined values
- Mutation APIs
- Immutable variants
- Reference-based operations

The generator should eliminate repetitive handwritten implementation patterns.

## Mathematical Features

Generated types should support capabilities such as:

- Static predefined values:
  - `Zero`
  - `One`
  - Unit vectors
- Efficient constructors
- Operator overloads
- In-place mutation
- Copy-based operations
- Reference-passing operations
- Strong typing
- SIMD-friendly memory layouts where possible

## Mutability Exploration

The architecture should explore multiple mutability models.

Examples include:

- Mutable structures
- Immutable structures
- Read-only wrappers
- Immutable variants
- Functional-style APIs
- Reference mutation APIs

Potential concepts:

- `Vector3`
- `ReadonlyVector3`
- `Vector3.Immutable`
- Immutable wrappers
- Value-semantic variants

## Performance-Oriented Design

The generated implementations should prioritize:

- Efficient value semantics
- Inlining-friendly operations
- Predictable memory behavior
- Minimal allocation overhead
- Reference mutation support
- SIMD compatibility where practical

The architecture should investigate trade-offs between ergonomics and performance.

## Geometry and Spatial Semantics

Geometry primitives introduce additional concerns the system must address, including:

- Coordinate semantics
- Dimensional consistency
- Generic numeric support
- Precision handling
- Mutable versus immutable representations
- Serialization implications
- API ergonomics

## Collision and Spatial Operations

The generated system should eventually support:

- Intersections
- Collision calculations
- Transformations
- Geometric queries
- Spatial operations

## Architectural Direction

The system should function as a declarative mathematical code synthesis architecture.

The architecture should prioritize:

- Mathematical DSLs
- Equation compilation
- Strongly typed generation
- Reusable mathematical definitions
- Systematic code synthesis
- Performance-oriented runtime implementations
- API ergonomics
- Generic math exploration

Rather than manually authored repetitive mathematical infrastructure.