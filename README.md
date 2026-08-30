# MonoGame-Style Math Vectors from a Source Generator

Generates mutable XNA/MonoGame-style two- and three-component vector structs for `float` and `double`.

```csharp
public static readonly VectorDefinitionSet All = new(
    Numerics:
    [
        new NumericVariant("float", "", "f"),
        new NumericVariant("double", "d", "d"),
    ],
    Vectors:
    [
        new VectorDefinition("Vector2", ["X", "Y"]),
        new VectorDefinition("Vector3", ["X", "Y", "Z"]),
    ]);
```

Generated `Vector3` (see `evidence/Vector3.g.cs`):

```csharp
public partial struct Vector3 : IEquatable<Vector3>
{
    public float X;
    public float Y;
    public float Z;

    public static readonly Vector3 UnitZ = new Vector3(0f, 0f, 1f);

    public static Vector3 operator +(Vector3 left, Vector3 right)
    {
        return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    }

    public void Add(Vector3 other)
    {
        X += other.X;
        Y += other.Y;
        Z += other.Z;
    }
    // ...
}
```

```sh
make build
make test
```

## Notes

- A constant definition table expands into `Vector2`, `Vector3`, `Vector2d`, and `Vector3d`.
- Generated types include fields, constructors, predefined values, equality, formatting, operators, and in-place operations.
- The generator is consumed as an analyzer and the consumer project contains no handwritten public vector types.
- The `netstandard2.0` generator includes an `IsExternalInit` shim for positional records.
