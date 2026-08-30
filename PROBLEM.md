# Problem

Mathematics and geometry libraries in C# often contain enormous amounts of repetitive implementation detail despite being fundamentally driven by structured mathematical definitions.

Historically, frameworks such as:

- XNA
- MonoGame
- `System.Numerics`
- Related numerical and geometry libraries

provided mature and ergonomic APIs for vectors, matrices, quaternions, and geometry primitives.

However, these systems typically required large amounts of handwritten repetitive code, including:

- Operator overloads
- Numeric type variants
- Conversion logic
- Geometric operations
- Mutation variants
- Immutable variants
- Overload permutations
- Utility methods
- Boilerplate structure

## Core Problem

Mathematical APIs are highly repetitive but structurally regular.

The same concepts repeatedly appear across:

- Numeric types
- Dimensional variants
- Mutable and immutable representations
- Geometry primitives
- Coordinate systems
- Transform operations

This creates a problem:

How can mathematical structures and operations be represented declaratively so that high-quality runtime implementations can be synthesized automatically?

## Scope

The system should explore implementations of:

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
- Collision and intersection primitives
- Supporting geometry and math structures

Across multiple numeric types including:

- `int`
- `float`
- `double`
- `byte`
- Potentially additional numeric representations

## Requirements

The system should support:

- Mathematical equation representation
- Source-generated implementations
- Multiple numeric backends
- Geometry primitives
- Mutation and immutability exploration
- Strong typing
- Performance-oriented APIs
- Operator overloads
- Efficient value semantics
- SIMD-friendly layouts where possible
- Reference-based operations
- Copy-based operations
- In-place mutation operations

## External Mathematical Definitions

The mathematical definitions themselves should ideally exist outside the generated runtime code.

Potential inputs include:

- Open mathematical specifications
- Existing equation representation formats
- Symbolic math systems
- Math DSLs
- Parser ecosystems
- Structured equation definition files

The implementation should prefer leveraging existing standards or ecosystems where practical rather than inventing unnecessary custom formats.

## Architectural Concerns

The system must explore multiple competing design concerns, including:

- Mutable versus immutable representations
- Performance versus ergonomics
- Generic numeric support
- Coordinate semantics
- Dimensional consistency
- Precision behavior
- Allocation behavior
- Reference passing semantics
- Serialization implications
- API usability

## Success Criteria

The system succeeds if it can:

- Represent mathematical structures declaratively
- Generate high-quality C# mathematical implementations
- Reduce repetitive handwritten math infrastructure
- Support multiple numeric types systematically
- Produce APIs comparable in usability and quality to XNA or MonoGame
- Preserve strong typing and performance-oriented semantics
- Generate ergonomic and predictable runtime behavior