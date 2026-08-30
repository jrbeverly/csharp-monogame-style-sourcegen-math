using System;
using System.Linq;
using System.Reflection;
using MonoGameMath;
using Xunit;

namespace MonoGameMath.Vectors.Tests;

public class VectorValueSemanticsTests
{
    [Fact]
    public void Vector2_predefined_values_have_correct_components()
    {
        Assert.Equal(0f, Vector2.Zero.X);
        Assert.Equal(0f, Vector2.Zero.Y);
        Assert.Equal(1f, Vector2.One.X);
        Assert.Equal(1f, Vector2.One.Y);
        Assert.Equal(1f, Vector2.UnitX.X);
        Assert.Equal(0f, Vector2.UnitX.Y);
        Assert.Equal(0f, Vector2.UnitY.X);
        Assert.Equal(1f, Vector2.UnitY.Y);
    }

    [Fact]
    public void Vector3_predefined_values_have_correct_components()
    {
        Assert.Equal(0f, Vector3.Zero.X);
        Assert.Equal(0f, Vector3.Zero.Y);
        Assert.Equal(0f, Vector3.Zero.Z);
        Assert.Equal(1f, Vector3.One.X);
        Assert.Equal(1f, Vector3.One.Y);
        Assert.Equal(1f, Vector3.One.Z);
        Assert.Equal(1f, Vector3.UnitX.X);
        Assert.Equal(0f, Vector3.UnitX.Y);
        Assert.Equal(0f, Vector3.UnitX.Z);
        Assert.Equal(0f, Vector3.UnitY.X);
        Assert.Equal(1f, Vector3.UnitY.Y);
        Assert.Equal(0f, Vector3.UnitY.Z);
        Assert.Equal(0f, Vector3.UnitZ.X);
        Assert.Equal(0f, Vector3.UnitZ.Y);
        Assert.Equal(1f, Vector3.UnitZ.Z);
    }

    [Fact]
    public void Vector2d_predefined_values_have_correct_components()
    {
        Assert.Equal(0d, Vector2d.Zero.X);
        Assert.Equal(0d, Vector2d.Zero.Y);
        Assert.Equal(1d, Vector2d.One.X);
        Assert.Equal(1d, Vector2d.One.Y);
        Assert.Equal(1d, Vector2d.UnitX.X);
        Assert.Equal(0d, Vector2d.UnitX.Y);
        Assert.Equal(0d, Vector2d.UnitY.X);
        Assert.Equal(1d, Vector2d.UnitY.Y);
    }

    [Fact]
    public void Vector3d_predefined_values_have_correct_components()
    {
        Assert.Equal(0d, Vector3d.Zero.X);
        Assert.Equal(0d, Vector3d.Zero.Y);
        Assert.Equal(0d, Vector3d.Zero.Z);
        Assert.Equal(1d, Vector3d.One.X);
        Assert.Equal(1d, Vector3d.One.Y);
        Assert.Equal(1d, Vector3d.One.Z);
        Assert.Equal(1d, Vector3d.UnitX.X);
        Assert.Equal(0d, Vector3d.UnitX.Y);
        Assert.Equal(0d, Vector3d.UnitX.Z);
        Assert.Equal(0d, Vector3d.UnitY.X);
        Assert.Equal(1d, Vector3d.UnitY.Y);
        Assert.Equal(0d, Vector3d.UnitY.Z);
        Assert.Equal(0d, Vector3d.UnitZ.X);
        Assert.Equal(0d, Vector3d.UnitZ.Y);
        Assert.Equal(1d, Vector3d.UnitZ.Z);
    }

    [Fact]
    public void Unit_vectors_match_the_component_list_of_each_type()
    {
        var vector2Units = typeof(Vector2)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(field => field.Name)
            .Where(name => name.StartsWith("Unit", StringComparison.Ordinal))
            .OrderBy(name => name)
            .ToArray();
        var vector3Units = typeof(Vector3)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(field => field.Name)
            .Where(name => name.StartsWith("Unit", StringComparison.Ordinal))
            .OrderBy(name => name)
            .ToArray();

        Assert.Equal(new[] { "UnitX", "UnitY" }, vector2Units);
        Assert.Equal(new[] { "UnitX", "UnitY", "UnitZ" }, vector3Units);
    }

    [Fact]
    public void Vector2_equal_components_are_equal_and_share_hash_code()
    {
        var left = new Vector2(1f, 2f);
        var right = new Vector2(1f, 2f);

        Assert.True(left == right);
        Assert.False(left != right);
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact]
    public void Vector2_differing_components_are_unequal()
    {
        var left = new Vector2(1f, 2f);
        var right = new Vector2(1f, 3f);

        Assert.False(left == right);
        Assert.True(left != right);
        Assert.False(left.Equals(right));
    }

    [Fact]
    public void Vector3_equal_components_are_equal_and_share_hash_code()
    {
        var left = new Vector3(1f, 2f, 3f);
        var right = new Vector3(1f, 2f, 3f);

        Assert.True(left == right);
        Assert.False(left != right);
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact]
    public void Vector3_differing_components_are_unequal()
    {
        var left = new Vector3(1f, 2f, 3f);
        var right = new Vector3(1f, 2f, 4f);

        Assert.False(left == right);
        Assert.True(left != right);
        Assert.False(left.Equals(right));
    }

    [Fact]
    public void Vector2d_equal_components_are_equal_and_share_hash_code()
    {
        var left = new Vector2d(1.5, 2.5);
        var right = new Vector2d(1.5, 2.5);

        Assert.True(left == right);
        Assert.False(left != right);
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact]
    public void Vector2d_differing_components_are_unequal()
    {
        var left = new Vector2d(1.5, 2.5);
        var right = new Vector2d(1.5, 3.5);

        Assert.False(left == right);
        Assert.True(left != right);
        Assert.False(left.Equals(right));
    }

    [Fact]
    public void Vector3d_equal_components_are_equal_and_share_hash_code()
    {
        var left = new Vector3d(1.5, 2.5, 3.5);
        var right = new Vector3d(1.5, 2.5, 3.5);

        Assert.True(left == right);
        Assert.False(left != right);
        Assert.True(left.Equals(right));
        Assert.True(left.Equals((object)right));
        Assert.Equal(left.GetHashCode(), right.GetHashCode());
    }

    [Fact]
    public void Vector3d_differing_components_are_unequal()
    {
        var left = new Vector3d(1.5, 2.5, 3.5);
        var right = new Vector3d(1.5, 2.5, 4.5);

        Assert.False(left == right);
        Assert.True(left != right);
        Assert.False(left.Equals(right));
    }

    [Fact]
    public void ToString_renders_all_components()
    {
        Assert.Equal("{X:1 Y:2}", new Vector2(1f, 2f).ToString());
        Assert.Equal("{X:1 Y:2 Z:3}", new Vector3(1f, 2f, 3f).ToString());
        Assert.Equal("{X:1.5 Y:2.5}", new Vector2d(1.5, 2.5).ToString());
        Assert.Equal("{X:1.5 Y:2.5 Z:3.5}", new Vector3d(1.5, 2.5, 3.5).ToString());
    }
}
