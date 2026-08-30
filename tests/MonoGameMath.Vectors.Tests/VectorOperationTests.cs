using MonoGameMath;
using Xunit;

namespace MonoGameMath.Vectors.Tests;

public class VectorOperationTests
{
    [Fact]
    public void Vector2_operators_are_component_wise()
    {
        var left = new Vector2(1f, 2f);
        var right = new Vector2(3f, 4f);

        Assert.Equal(new Vector2(4f, 6f), left + right);
        Assert.Equal(new Vector2(-2f, -2f), left - right);
        Assert.Equal(new Vector2(-1f, -2f), -left);
        Assert.Equal(new Vector2(2f, 4f), left * 2f);
        Assert.Equal(new Vector2(2f, 4f), 2f * left);
    }

    [Fact]
    public void Vector3_operators_are_component_wise()
    {
        var left = new Vector3(1f, 2f, 3f);
        var right = new Vector3(4f, 5f, 6f);

        Assert.Equal(new Vector3(5f, 7f, 9f), left + right);
        Assert.Equal(new Vector3(-3f, -3f, -3f), left - right);
        Assert.Equal(new Vector3(-1f, -2f, -3f), -left);
        Assert.Equal(new Vector3(2f, 4f, 6f), left * 2f);
        Assert.Equal(new Vector3(2f, 4f, 6f), 2f * left);
    }

    [Fact]
    public void Vector2d_operators_are_component_wise()
    {
        var left = new Vector2d(1.5, 2.5);
        var right = new Vector2d(3.5, 4.5);

        Assert.Equal(new Vector2d(5.0, 7.0), left + right);
        Assert.Equal(new Vector2d(-2.0, -2.0), left - right);
        Assert.Equal(new Vector2d(-1.5, -2.5), -left);
        Assert.Equal(new Vector2d(3.0, 5.0), left * 2.0);
        Assert.Equal(new Vector2d(3.0, 5.0), 2.0 * left);
    }

    [Fact]
    public void Vector3d_operators_are_component_wise()
    {
        var left = new Vector3d(1.5, 2.5, 3.5);
        var right = new Vector3d(4.5, 5.5, 6.5);

        Assert.Equal(new Vector3d(6.0, 8.0, 10.0), left + right);
        Assert.Equal(new Vector3d(-3.0, -3.0, -3.0), left - right);
        Assert.Equal(new Vector3d(-1.5, -2.5, -3.5), -left);
        Assert.Equal(new Vector3d(3.0, 5.0, 7.0), left * 2.0);
        Assert.Equal(new Vector3d(3.0, 5.0, 7.0), 2.0 * left);
    }

    [Fact]
    public void Vector2_Add_and_Subtract_return_new_values_without_touching_inputs()
    {
        var left = new Vector2(1f, 2f);
        var right = new Vector2(3f, 4f);

        var sum = Vector2.Add(left, right);
        var difference = Vector2.Subtract(left, right);

        Assert.Equal(new Vector2(4f, 6f), sum);
        Assert.Equal(new Vector2(-2f, -2f), difference);
        Assert.Equal(new Vector2(1f, 2f), left);
        Assert.Equal(new Vector2(3f, 4f), right);
    }

    [Fact]
    public void Vector3_Add_and_Subtract_return_new_values_without_touching_inputs()
    {
        var left = new Vector3(1f, 2f, 3f);
        var right = new Vector3(4f, 5f, 6f);

        var sum = Vector3.Add(left, right);
        var difference = Vector3.Subtract(left, right);

        Assert.Equal(new Vector3(5f, 7f, 9f), sum);
        Assert.Equal(new Vector3(-3f, -3f, -3f), difference);
        Assert.Equal(new Vector3(1f, 2f, 3f), left);
        Assert.Equal(new Vector3(4f, 5f, 6f), right);
    }

    [Fact]
    public void Vector2d_Add_and_Subtract_return_new_values_without_touching_inputs()
    {
        var left = new Vector2d(1.5, 2.5);
        var right = new Vector2d(3.5, 4.5);

        var sum = Vector2d.Add(left, right);
        var difference = Vector2d.Subtract(left, right);

        Assert.Equal(new Vector2d(5.0, 7.0), sum);
        Assert.Equal(new Vector2d(-2.0, -2.0), difference);
        Assert.Equal(new Vector2d(1.5, 2.5), left);
        Assert.Equal(new Vector2d(3.5, 4.5), right);
    }

    [Fact]
    public void Vector3d_Add_and_Subtract_return_new_values_without_touching_inputs()
    {
        var left = new Vector3d(1.5, 2.5, 3.5);
        var right = new Vector3d(4.5, 5.5, 6.5);

        var sum = Vector3d.Add(left, right);
        var difference = Vector3d.Subtract(left, right);

        Assert.Equal(new Vector3d(6.0, 8.0, 10.0), sum);
        Assert.Equal(new Vector3d(-3.0, -3.0, -3.0), difference);
        Assert.Equal(new Vector3d(1.5, 2.5, 3.5), left);
        Assert.Equal(new Vector3d(4.5, 5.5, 6.5), right);
    }

    [Fact]
    public void Vector2_instance_Add_and_Subtract_mutate_the_target_in_place()
    {
        var target = new Vector2(1f, 2f);
        var other = new Vector2(3f, 4f);

        target.Add(other);
        Assert.Equal(new Vector2(4f, 6f), target);
        Assert.Equal(new Vector2(3f, 4f), other);

        target.Subtract(other);
        Assert.Equal(new Vector2(1f, 2f), target);
        Assert.Equal(new Vector2(3f, 4f), other);
    }

    [Fact]
    public void Vector3_instance_Add_and_Subtract_mutate_the_target_in_place()
    {
        var target = new Vector3(1f, 2f, 3f);
        var other = new Vector3(4f, 5f, 6f);

        target.Add(other);
        Assert.Equal(new Vector3(5f, 7f, 9f), target);
        Assert.Equal(new Vector3(4f, 5f, 6f), other);

        target.Subtract(other);
        Assert.Equal(new Vector3(1f, 2f, 3f), target);
        Assert.Equal(new Vector3(4f, 5f, 6f), other);
    }

    [Fact]
    public void Vector2d_instance_Add_and_Subtract_mutate_the_target_in_place()
    {
        var target = new Vector2d(1.5, 2.5);
        var other = new Vector2d(3.5, 4.5);

        target.Add(other);
        Assert.Equal(new Vector2d(5.0, 7.0), target);
        Assert.Equal(new Vector2d(3.5, 4.5), other);

        target.Subtract(other);
        Assert.Equal(new Vector2d(1.5, 2.5), target);
        Assert.Equal(new Vector2d(3.5, 4.5), other);
    }

    [Fact]
    public void Vector3d_instance_Add_and_Subtract_mutate_the_target_in_place()
    {
        var target = new Vector3d(1.5, 2.5, 3.5);
        var other = new Vector3d(4.5, 5.5, 6.5);

        target.Add(other);
        Assert.Equal(new Vector3d(6.0, 8.0, 10.0), target);
        Assert.Equal(new Vector3d(4.5, 5.5, 6.5), other);

        target.Subtract(other);
        Assert.Equal(new Vector3d(1.5, 2.5, 3.5), target);
        Assert.Equal(new Vector3d(4.5, 5.5, 6.5), other);
    }

    [Fact]
    public void copy_returning_and_in_place_operations_share_the_same_math()
    {
        var left = new Vector2(1f, 2f);
        var right = new Vector2(3f, 4f);

        var sum = Vector2.Add(left, right);
        left.Add(right);

        Assert.Equal(new Vector2(1f, 2f) + new Vector2(3f, 4f), sum);
        Assert.Equal(sum, left);
    }
}
