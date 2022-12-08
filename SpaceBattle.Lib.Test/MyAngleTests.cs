using Moq;

namespace SpaceBattle.Lib.Test;

public class MyAngleTests
{

    [Fact]
    public void Successful_Angles_Addition()
    {
        MyAngle a = new MyAngle(30, 1);
        MyAngle b = new MyAngle(45, 1);
        Assert.Equal(new MyAngle(75, 1), a + b);

    }

    [Fact]
    public void Failed_Angle_Equality()
    {
        MyAngle a = new MyAngle(30, 1);
        MyAngle b = new MyAngle(45, 1);
        Assert.False(a == b);
    }

    [Fact]
    public void Successful_Angle_Equality()
    {
        MyAngle a = new MyAngle(-30, 1);
        MyAngle b = new MyAngle(90, -3);
        Assert.True(a == b);
    }

    [Fact]
    public void Successful_Angle_Inequality()
    {
        MyAngle a = new MyAngle(30, 1);
        MyAngle b = new MyAngle(45, 1);
        Assert.True(a != b);
    }

    [Fact]
    public void Successful_GetHashGood()
    {
        MyAngle a = new MyAngle(-30, -1);
        MyAngle b = new MyAngle(90, 3);
        Assert.True(a.GetHashCode() == b.GetHashCode());
    }

    [Fact]
    public void Failed_Angle_EqualsMethod()
    {
        MyAngle a = new MyAngle(-30, -1);
        int b = 1;
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ZeroDenominator_In_Angle()
    {
        Assert.Throws<ArgumentException>(()=> new MyAngle(1,0));
    }
}
