using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class GetHashCodeStrategyTests
{
    [Fact]
    public void EqualHashCodesForDifferentPermutationsOfObjects()
    {
        var a = new List<Type>() { typeof(IOException), typeof(ArgumentException), typeof(StartCommand) };
        var b = new List<Type>() { typeof(StartCommand), typeof(ArgumentException), typeof(IOException) };
        var c = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };

        var strategy = new GetHashCodeStrategy();

        Assert.Equal(strategy.RunStrategy(a), strategy.RunStrategy(b));
        Assert.Equal(strategy.RunStrategy(c), strategy.RunStrategy(b));
        Assert.Equal(strategy.RunStrategy(a), strategy.RunStrategy(c));
    }
    [Fact]
    public void NotEqualHashCodesForDifferentObjects()
    {
        var a = new List<Type>() { typeof(IOException), typeof(ArgumentException), typeof(StartCommand) };
        var b = new List<Type>() { typeof(StartCommand), typeof(ArgumentException) };

        var strategy = new GetHashCodeStrategy();

        Assert.NotEqual(strategy.RunStrategy(a), strategy.RunStrategy(b));
    }
    [Fact]
    public void EqualHashCodesForSameObjects()
    {
        var a = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };
        var b = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };
        var c = new List<Type>() { typeof(ArgumentException), typeof(StartCommand), typeof(IOException) };

        var strategy = new GetHashCodeStrategy();

        Assert.Equal(strategy.RunStrategy(a), strategy.RunStrategy(b));
        Assert.Equal(strategy.RunStrategy(c), strategy.RunStrategy(b));
        Assert.Equal(strategy.RunStrategy(a), strategy.RunStrategy(c));
    }
}
