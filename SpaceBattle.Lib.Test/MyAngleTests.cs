using Moq;

namespace SpaceBattle.Lib.Test;

public class MyAngleTests
{

    [Fact]
    public void SubstractionGood()
    {
        MyAngle a = new MyAngle(30, 1);
        MyAngle b = new MyAngle(45, 1);
        Assert.Equal(new MyAngle(75, 1), a + b);

    }

    [Fact]
    public void ComparisonBad2()
    {
        MyAngle a = new MyAngle(30, 1);
        MyAngle b = new MyAngle(45, 1);
        Assert.False(a == b);
    }

    [Fact]
    public void ComparisonGood()
    {
        MyAngle a = new MyAngle(-30, 1);
        MyAngle b = new MyAngle(90, -3);
        Assert.True(a == b);
    }

    [Fact]
    public void NotComparisonGood()
    {
        MyAngle a = new MyAngle(30, 1);
        MyAngle b = new MyAngle(45, 1);
        Assert.True(a != b);
    }

    [Fact]
    public void GetHashGood()
    {
        MyAngle a = new MyAngle(-30, -1);
        MyAngle b = new MyAngle(90, 3);
        Assert.True(a.GetHashCode() == b.GetHashCode());
    }

    [Fact]
    public void EqualsBad()
    {
        MyAngle a = new MyAngle(-30, -1);
        int b = 1;
        Assert.False(a.Equals(b));
    }

    [Fact]
    public void ZeroException()
    {
        Assert.Throws<Exception>(()=> new MyAngle(1,0));
    }
}
