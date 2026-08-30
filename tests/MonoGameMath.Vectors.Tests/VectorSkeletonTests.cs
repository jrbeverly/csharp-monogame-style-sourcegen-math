using MonoGameMath;
using Xunit;

namespace MonoGameMath.Vectors.Tests;

public class VectorSkeletonTests
{
    [Fact]
    public void Vector2_float_constructs_and_reads_components()
    {
        var v = new Vector2(1f, 2f);
        Assert.Equal(1f, v.X);
        Assert.Equal(2f, v.Y);
    }

    [Fact]
    public void Vector3_float_constructs_and_reads_components()
    {
        var v = new Vector3(1f, 2f, 3f);
        Assert.Equal(1f, v.X);
        Assert.Equal(2f, v.Y);
        Assert.Equal(3f, v.Z);
    }

    [Fact]
    public void Vector2d_constructs_and_reads_components()
    {
        var v = new Vector2d(1.5, 2.5);
        Assert.Equal(1.5, v.X);
        Assert.Equal(2.5, v.Y);
    }

    [Fact]
    public void Vector3d_constructs_and_reads_components()
    {
        var v = new Vector3d(1.5, 2.5, 3.5);
        Assert.Equal(1.5, v.X);
        Assert.Equal(2.5, v.Y);
        Assert.Equal(3.5, v.Z);
    }
}
