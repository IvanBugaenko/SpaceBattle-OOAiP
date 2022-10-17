using Moq;

namespace SpaceBattle.Lib.Test;

public class VectorTests
{
    [Fact]
    public void SetIndex()
    {
        Vector A = new Vector(0, 0, 0, 0);
        A[0] = 1;
        Assert.Equal(1, A[0]);
    }

    [Fact]
    public void SumErr()
    {
        Vector A = new Vector(1, 0, 0, 0);
        Vector B = new Vector(1, 1);
        Assert.Throws<ArgumentException>(() => A + B);
    }

    [Fact]
    public void SubErr()
    {
        Vector A = new Vector(0, 0, 0, 0);
        Vector B = new Vector(1, 1);
        Assert.Throws<ArgumentException>(() => A - B);
    }

    [Fact]
    public void SubstractionGood()
    {
        Vector A = new Vector(0, 0, 0, 0);
        Vector B = new Vector(1, 1, 1, 1);
        Assert.Equal(new Vector(-1, -1, -1, -1), A - B);
    }

    [Fact]
    public void MultiplicationGood()
    {
        Vector A = new Vector(1, 1, 1, 2);
        int alpha = 3;
        Assert.Equal(new Vector(3, 3, 3, 6), alpha * A);
    }

    [Fact]
    public void ComparisonBad1()
    {
        Vector A = new Vector(0, 0, 0, 0);
        Vector B = new Vector(1, 1);
        Assert.False(A == B);
    }

    [Fact]
    public void ComparisonBad2()
    {
        Vector A = new Vector(1, 2, 3, 4);
        Vector B = new Vector(1, 2, 4, 4);
        Assert.False(A == B);
    }

    [Fact]
    public void ComparisonGood()
    {
        Vector A = new Vector(0, 0, 0, 0);
        Vector B = new Vector(0, 0, 0, 0);
        Assert.True(A == B);
    }

    [Fact]
    public void NotComparisonGood()
    {
        Vector A = new Vector(0, 1, 0, 0);
        Vector B = new Vector(0, 0, 0, 0);
        Assert.True(A != B);
    }

    [Fact]
    public void GetHashGood()
    {
        Vector A = new Vector(0, 1, 0, 0);
        Vector B = new Vector(0, 1, 0, 0);
        Assert.True(A.GetHashCode() == B.GetHashCode());
    }

    [Fact]
    public void GetHashBad()
    {
        Vector A = new Vector(0, 1, 7, 0);
        Vector B = new Vector(0, 1, 0, 0);
        Assert.True(A.GetHashCode() != B.GetHashCode());
    }

    [Fact]
    public void EqualsBad()
    {
        Vector A = new Vector(0, 1, 0, 0);
        int a = 1;
        Assert.False(A.Equals(a));
    }

    [Fact]
    public void ZeroLen()
    {
        Assert.Throws<Exception>(() => new Vector());
    }
}