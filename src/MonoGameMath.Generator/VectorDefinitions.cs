namespace MonoGameMath.Generator;

internal sealed record NumericVariant(string Keyword, string NameSuffix, string LiteralSuffix);

internal sealed record VectorDefinition(string Name, string[] Components);

internal sealed record VectorDefinitionSet(
    NumericVariant[] Numerics,
    VectorDefinition[] Vectors);

internal static class VectorDefinitions
{
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
}
