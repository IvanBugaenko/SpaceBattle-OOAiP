using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class ExceptionHandlerStrategyTests
{
    List<Type> listWithOneCommand = new List<Type>() { typeof(MoveCommand) };
    List<Type> listWithOneCommandAndOneException = new List<Type>() { typeof(RotateCommand), typeof(ArgumentException) };
    List<Type> listWithOneException = new List<Type>() { typeof(IOException) };
    List<Type> listThatHashNotInTree = new List<Type>() { typeof(IOException), typeof(ArgumentException) };
    public ExceptionHandlerStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockHandler = new Mock<IHandler>();

        var tree = new Dictionary<int, IHandler>()
        {
            {0, mockHandler.Object},
            {1, mockHandler.Object},
            {2, mockHandler.Object},
            {12345, mockHandler.Object}
        };

        var mockStrategyReturnsExseptionTree = new Mock<IStrategy>();
        mockStrategyReturnsExseptionTree.Setup(m => m.RunStrategy()).Returns(tree);

        var mockStrategyReturnsHash = new Mock<IStrategy>();
        mockStrategyReturnsHash.Setup(m => m.RunStrategy(listWithOneCommand)).Returns(1);
        mockStrategyReturnsHash.Setup(m => m.RunStrategy(listWithOneCommandAndOneException)).Returns(2);
        mockStrategyReturnsHash.Setup(m => m.RunStrategy(listWithOneException)).Returns(12345);
        mockStrategyReturnsHash.Setup(m => m.RunStrategy(listThatHashNotInTree)).Returns(4);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetExceptionTree", (object[] args) => mockStrategyReturnsExseptionTree.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetHashCode", (object[] args) => mockStrategyReturnsHash.Object.RunStrategy(args)).Execute();
    }

    [Fact]
    public void SuccesfulFindHandlerFromTreeWithOnlyOneCommand()
    {
        var a = new List<Type>() { typeof(MoveCommand) };

        var strategy = new FindExceptionHandlerStrategy();

        Assert.NotNull(strategy.RunStrategy(a));
    }

    [Fact]
    public void SuccesfulFindHandlerFromTreeWithOneCommandAndOneException()
    {
        var b = new List<Type>() { typeof(RotateCommand), typeof(ArgumentException) };

        var strategy = new FindExceptionHandlerStrategy();

        Assert.NotNull(strategy.RunStrategy(b));
    }

    [Fact]
    public void SuccesfulFindHandlerFromTreeWithOnlyOneException()
    {
        var c = new List<Type>() { typeof(IOException) };

        var strategy = new FindExceptionHandlerStrategy();

        Assert.NotNull(strategy.RunStrategy(c));
    }

    [Fact]
    public void SuccesfulFindDefaultHandler()
    {
        var d = new List<Type>() { typeof(IOException), typeof(ArgumentException) };

        var strategy = new FindExceptionHandlerStrategy();

        Assert.NotNull(strategy.RunStrategy(d));
    }
}
