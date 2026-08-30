# Vision

Build a source-generator-driven mathematical and geometry framework for C# inspired by the design quality and ergonomics of XNA, MonoGame, and `System.Numerics`.

The goal is to create a declarative mathematical system where mathematical definitions exist independently from the runtime implementation and source generators synthesize the actual library code automatically.

## Desired Architecture

The system should separate:

- Mathematical definitions
- Equation semantics
- Geometry definitions

From:

- Runtime implementations
- Numeric variants
- Operator overloads
- Generated APIs

The source generators become a mathematical compilation layer that translates structured mathematical definitions into optimized C# code.

## Mathematical Definition Model

The ideal architecture uses externally defined mathematical representations.

Potential sources include:

- Existing math DSLs
- Symbolic equation systems
- Open mathematical specifications
- Structured equation definitions
- Existing parser ecosystems

The generator should consume those definitions and synthesize strongly typed runtime implementations.

## Generated Runtime Capabilities

The generated math library should provide features expected from mature numerical frameworks, including:

- Ergonomic APIs
- Efficient constructors
- Static predefined values
- Operator overloads
- Mutable operations
- Immutable operations
- Reference-based APIs
- SIMD-friendly layouts where possible
- Strong value semantics

The resulting library should feel comparable in quality and usability to XNA or MonoGame.

## Multi-Type Mathematical Generation

The architecture should systematically generate implementations across multiple numeric types.

Examples include:

- `float`
- `double`
- `int`
- `byte`

The system should avoid manually duplicating implementations for each numeric backend.

## Mutability Exploration

The system should explore multiple mathematical usage models, including:

- Mutable vectors
- Immutable vectors
- Read-only wrappers
- Functional APIs
- Performance-oriented mutation APIs

The goal is to investigate how different mutability assumptions affect:

- Performance
- Allocation behavior
- Ergonomics
- API clarity
- Numerical workflows

## Geometry and Spatial Systems

The exploration should extend beyond vectors and matrices into geometry and spatial constructs.

Examples include:

- Rectangles
- Triangles
- Bounding boxes
- Rays
- Planes
- Spheres
- Collision systems
- Transformations

These systems force the architecture to address more advanced concerns such as:

- Coordinate semantics
- Dimensional correctness
- Precision trade-offs
- Generic numeric support
- Spatial consistency

## Source Generation as Mathematical Synthesis

The broader architectural vision is to treat mathematics libraries as synthesized infrastructure rather than handwritten repetitive implementations.

Instead of manually implementing every:

- Operator
- Overload
- Numeric variant
- Geometry type
- Conversion path

The system should derive them from declarative mathematical definitions.

## Long-Term Direction

The long-term goal is a declarative mathematical framework where:

- Mathematical constructs are defined externally.
- Source generators synthesize optimized implementations.
- Geometry systems are generated systematically.
- Numeric variants are generated automatically.
- APIs remain ergonomic and performant.

The resulting runtime libraries should provide the quality, usability, and performance characteristics expected from mature game and numerical frameworks while dramatically reducing repetitive handwritten infrastructure.