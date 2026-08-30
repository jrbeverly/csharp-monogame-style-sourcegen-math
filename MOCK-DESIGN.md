# Mock Design

## Purpose

This document evaluates whether the direction described in [PROBLEM.md](PROBLEM.md), [TECHNICAL.md](TECHNICAL.md), and [VISION.md](VISION.md) is conceptually viable, where the main architectural boundaries should sit, and which unresolved decisions need to be addressed before implementation hardens.

The goal here is not to define a final schema or lock the repository into a concrete implementation. The goal is to answer whether the proposed system can realistically work, what shape it should likely take, and where the major risks live.

## Executive Summary

The proposed system is fundamentally implementable.

The strongest version of the idea is not "generate math code directly from equations," but instead:

1. Define mathematical and geometric concepts declaratively.
2. Normalize those definitions into a project-owned semantic model.
3. Apply API policy decisions such as mutability, naming, overload families, and numeric support.
4. Use C# source generators to synthesize the public runtime surface.

That architecture is viable and aligned with the repository vision.

The main risks are not around whether C# can generate code. It can. The harder questions are:

- how numeric types differ in capability and semantics
- how much mutability variation should exist in the public API
- how geometry semantics are represented beyond raw equations
- whether external standards can express enough information to drive an ergonomic C# API
- where source generators stop being the right tool and a separate compile step becomes useful

The project becomes much less viable if it tries to solve all of the following at once:

- vectors, matrices, quaternions, and collision-heavy geometry
- float, double, int, byte, and future numerics with equal fidelity
- mutable and immutable public type families
- ergonomic APIs matching XNA or MonoGame
- direct consumption of arbitrary third-party mathematical formats

The direction is viable if it is phased and if the architecture separates mathematical truth from C# API projection.

## Design Intent Interpreted From the Source Documents

The three input documents describe a system with four consistent priorities:

- eliminate repetitive handwritten mathematical infrastructure
- preserve the API quality associated with XNA, MonoGame, and `System.Numerics`
- keep mathematical definitions outside the generated runtime implementation
- support multiple numeric and geometric families without cloning logic by hand

They also imply several non-trivial constraints:

- performance matters, including value semantics and `ref`-style operations
- strong typing matters, especially for geometry and spatial concepts
- generated code should remain ergonomic, not merely mechanically correct
- the repository is exploring mutability models rather than assuming one up front
- the system should prefer existing ecosystems where practical, but not at the expense of architectural clarity

## Viability Assessment

### Short Answer

Yes, the system is viable, but only with a layered architecture and a staged scope.

### Why It Is Viable

- Mathematical and geometric libraries contain large regions of structured repetition that are well suited to declarative generation.
- Roslyn source generators are a strong fit for compile-time synthesis of repetitive C# APIs.
- Many desirable runtime variations such as operators, conversions, predefined constants, overload families, and mutation helpers can be generated reliably once the semantics are explicit.
- External mathematical definitions can be made practical if they feed a canonical internal representation rather than driving code generation directly.

### Why It Is Not Trivially Viable

- Many operations are not equally meaningful across all numeric types.
- Geometry behavior depends on conventions and invariants that raw equations do not capture well by themselves.
- API ergonomics are a policy problem, not just a mathematical one.
- Source generators are good at synthesis, but less comfortable as a full symbolic mathematics platform.

The project is therefore viable as a mathematical code synthesis architecture, but not as a purely equation-to-code pipeline with no intermediate semantic layer.

## High-Level Architectural Shape

The cleanest conceptual shape is a five-layer system.

### 1. Definition Layer

This layer contains declarative mathematical and geometric descriptions that are independent from generated C# runtime code.

Examples of the kinds of things this layer should express:

- types and dimensions
- fields and components
- invariants and semantic constraints
- operations and formulas
- relationships between types
- coordinate or spatial meaning where relevant

This layer may accept multiple authoring sources:

- curated project-native definitions
- imported structured math descriptions
- adapters from external DSLs or symbolic systems

The important point is that external inputs should be treated as sources, not as the canonical runtime truth.

### 2. Semantic Model Layer

This is the most important architectural boundary.

The repository should own a canonical intermediate representation that captures:

- mathematical operations
- type families
- numeric capability requirements
- geometry semantics
- invariants
- projection hints needed for code generation

This layer protects the project from overcommitting to any one external format and keeps the generator target stable even if authoring inputs evolve.

### 3. API Policy Layer

The semantic model alone is not enough to generate a good C# library.

A separate policy layer should determine:

- naming conventions
- mutability projections
- which overload families exist
- which operations are exposed as operators versus methods
- which numeric families support which APIs
- which predefined values should exist
- which APIs are in-place, copy-based, or `ref`-based

This is where XNA-style ergonomics and runtime design choices belong. They should not be baked into the mathematical definitions themselves.

### 4. Generation Layer

This layer transforms semantic definitions plus API policy into generated C# source.

Likely responsibilities:

- emit mathematical types
- emit operators and overloads
- emit constructors and conversions
- emit static predefined values
- emit mutation-oriented helpers
- emit numeric-specialized variants

An incremental source generator is a strong default here. It keeps the developer workflow close to standard C# builds and supports compile-time validation feedback.

### 5. Runtime Support Layer

Not everything should be generated.

A small handwritten runtime support layer is likely still necessary for:

- shared attributes or annotations
- low-level helper utilities
- numeric capability helpers
- optional interoperability shims
- pieces where hand-authored performance tuning is more practical than generation

The generated output should depend on a small stable support surface rather than trying to synthesize every supporting concern from scratch.

## Proposed Conceptual Data Flow

The intended data flow should look like this:

1. Mathematical or geometric definitions are authored or imported.
2. A parser or adapter normalizes them into a project-owned semantic model.
3. Validation checks confirm type consistency, dimensional rules, and capability requirements.
4. API policy determines how those semantics should appear in public C#.
5. Source generators synthesize runtime code.
6. Tests and benchmarks validate correctness, usability, and performance expectations.

This flow gives the repository a clear separation between authoring, semantic meaning, public API design, and generated implementation.

## Major Design Boundaries

### Mathematical Semantics vs API Ergonomics

This is the most important separation in the whole design.

The mathematical definition of vector addition is not the same thing as the decision to expose:

- `operator +`
- `Add`
- `Add(ref left, ref right, out result)`
- an in-place mutation API

Those are API projections of the same underlying operation. If the repository does not separate these concerns early, the generator will become rigid and difficult to reason about.

### Geometry Semantics vs Formula Semantics

Geometry primitives require more than equations.

For example, types such as rectangles, bounding boxes, planes, rays, and spheres also need explicit semantic decisions around:

- coordinate conventions
- boundary inclusivity
- normalization assumptions
- valid construction invariants
- transformation behavior
- interpretation of precision and tolerance

These decisions should be first-class in the semantic model rather than hidden in generated method bodies.

### Numeric Capability vs Numeric Type Name

Supporting `float`, `double`, `int`, and `byte` does not mean each type should receive the same public API.

The system likely needs capability families such as:

- floating-point numerics
- integral numerics
- storage-oriented or packed numerics

Without that distinction, the generator will either produce invalid or awkward APIs, or will fill the library with exceptions and special cases.

## Recommended Repository Shape

The repository does not need to adopt these exact names, but conceptually it should likely separate concerns like this:

- `definitions/`: declarative mathematical and geometric inputs
- `src/Definitions` or `src/Model`: semantic model, validators, and import adapters
- `src/Generator`: Roslyn source generator and generation policies
- `src/Runtime`: small handwritten support library for generated code
- `tests/`: compile tests, snapshot tests, property tests, and reference comparisons
- `benchmarks/`: targeted performance checks against expectations or reference libraries
- `docs/`: architecture notes, decisions, and generation examples

The main idea is that definitions, semantic meaning, generation policy, and runtime support should not collapse into one project.

## Key Technical and Architectural Risks

### 1. External Definitions May Not Be Rich Enough

Existing mathematical formats may help describe formulas, but they may not express:

- mutability strategy
- operator policy
- C# naming expectations
- geometric invariants
- coordinate conventions

This is not a reason to avoid external ecosystems. It is a reason not to depend on them as the canonical design language.

### 2. Numeric Parity Is a Trap

Some operations make sense only for floating-point types or have radically different behavior on integral types.

Examples of likely pressure points:

- normalization
- inverse or division-heavy operations
- trigonometric functions
- matrix inversion
- geometric intersection tolerance behavior
- overflow and rounding

If the architecture assumes every numeric backend gets equivalent APIs, design quality will degrade quickly.

### 3. Mutability Explosion Can Damage Usability

The documents intentionally explore mutable and immutable representations, but generating every variation for every type may create an incoherent library.

The system should likely distinguish between:

- the canonical semantic definition of an operation
- one or more public projection styles

That keeps exploration possible without multiplying conceptual complexity unnecessarily.

### 4. Source Generators Are Powerful but Not Infinite

Source generators are a good fit for code synthesis, but they are not automatically the best place for:

- heavy symbolic simplification
- very large generated surfaces
- advanced design-time diagnostics across many imported assets
- long-running preprocessing workflows

This does not block the project. It means the architecture should leave room for a future pre-generation or compilation step if scale demands it.

### 5. SIMD Expectations Need Careful Framing

"SIMD-friendly" is achievable.

"Automatically SIMD-accelerated everywhere" is a much stronger claim and may not hold uniformly across generated types, runtime targets, JIT behavior, or numeric families.

The design should treat layout and generated patterns as SIMD-compatible where practical, while avoiding overpromising universal acceleration.

### 6. Geometry Correctness Depends on Explicit Conventions

Matrix orientation, handedness, angle units, transformation order, and containment rules are all correctness-affecting choices.

If these are not chosen explicitly, generated geometry code may be internally consistent but still unusable or surprising.

## Assumptions That Need Validation

The current vision appears to assume the following. They may be true, but they should be validated explicitly:

- source generators are sufficient as the main synthesis mechanism for the intended scale
- external mathematical definitions can be normalized without excessive loss of meaning
- a single architecture can support both ergonomic APIs and broad numeric variation
- XNA or MonoGame style ergonomics can survive systematic generation without becoming bloated
- geometry primitives can be represented declaratively enough to avoid falling back to large amounts of handwritten special logic
- the repository can support mutability exploration without fragmenting the public API story

## Unresolved Decisions That Matter Early

These are not implementation details. They affect the viability of the architecture itself.

### 1. What Is the Canonical Authoring Model?

Will the repository primarily:

- author definitions in a project-native format
- import from external formats
- or support both, with an internal canonical representation

The third option appears strongest, but it should be chosen intentionally.

### 2. What Is the Primary Public API Style?

When XNA, MonoGame, and `System.Numerics` differ, which philosophy wins?

Without a primary API lineage, generation policy will drift.

### 3. Which Numeric Types Are First-Class Arithmetic Types?

Is `byte` intended to be a full arithmetic backend, a packed storage type, or a limited experimental target?

This choice meaningfully changes the generator strategy.

### 4. How Many Mutability Models Should Ship Publicly?

Is mutability exploration an internal experiment, or is the goal to expose multiple public projections in the shipped library?

The answer affects naming, discoverability, and API surface control.

### 5. What Geometry Conventions Are Fixed Globally?

Examples include:

- matrix layout and multiplication order
- left-handed versus right-handed assumptions
- radians versus degrees
- boundary rules for containment and intersection

These should be chosen early enough to guide semantic modeling.

### 6. Where Should Generated Code Live?

Is the generator primarily intended to:

- generate code for this repository's own runtime library
- or be consumed by downstream libraries and applications

Both are possible, but they imply different packaging and workflow expectations.

## Recommended Phasing

The vision is broad enough that staged proof is important.

### Phase 1: Prove the Core Pipeline

Focus on:

- `Vector2`, `Vector3`, `Vector4`
- matrix and quaternion basics
- `float` and `double`
- a single clear mutability strategy

Success here should prove:

- the semantic model works
- the generator can produce ergonomic APIs
- performance-oriented patterns remain practical

### Phase 2: Add Semantic Complexity

Expand into:

- rectangles
- rays
- planes
- bounding volumes
- a small set of collision or intersection operations

This phase validates whether geometry semantics can remain declarative without collapsing into ad hoc handwritten logic.

### Phase 3: Expand Numeric Strategy and API Variants

Only after the first two phases should the repository broaden:

- integral backends
- storage-oriented numerics
- alternate mutability projections
- more aggressive overload or conversion families

### Phase 4: Deepen External Definition Support

Once the internal semantic model is stable, external adapters become safer and more valuable.

At that point, the project can make better decisions about how much of the authoring experience should rely on external ecosystems.

## Clarifying Questions

These questions are important enough to shape the next design pass:

1. Is the primary deliverable a generated runtime library maintained in this repository, or a reusable generator platform meant for downstream consumers?
2. Which API lineage should be treated as the default when ergonomics conflict: XNA, MonoGame, or `System.Numerics`?
3. Should `int` and `byte` be treated as true arithmetic peers to `float` and `double`, or as narrower experimental or storage-oriented targets?
4. Is public support for multiple mutability models a core product goal, or is mutability exploration mainly for internal evaluation?
5. Which matrix and transform conventions should the library assume globally?
6. Is deterministic cross-platform behavior a requirement, or is performance-oriented best effort acceptable where runtime/JIT behavior differs?
7. Should external mathematical definitions be human-authored inside this repository, imported from external sources, or both?

## Final Assessment

The proposed system is conceptually coherent and worth pursuing.

Its strongest form is a declarative mathematical synthesis pipeline with:

- external or semi-external definitions
- a project-owned semantic intermediate model
- a separate API policy layer
- source-generated C# runtime code
- a small handwritten runtime support surface

The biggest blockers are not fundamental impossibilities. They are unresolved design choices around semantics, numeric capability, mutability scope, and public API lineage.

If those are addressed early and the first implementation phase stays intentionally narrow, the architecture described in the repository documents can realistically work.
