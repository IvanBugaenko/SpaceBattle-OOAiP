using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class AddExceptionHandlerStrategyTests
{
    public AddExceptionHandlerStrategyTests()
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

        var StrategyReturnsHash = new GetHashCodeStrategy();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetExceptionTree", (object[] args) => mockStrategyReturnsExseptionTree.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetHashCode", (object[] args) => StrategyReturnsHash.RunStrategy(args)).Execute();
    }

    [Fact]
    public void SuccesfulTryAddHandlerInTree()
    {
        var handler = new Mock<IHandler>();

        var cmd = new AddHandlerToExceptionTreeCommand(new List<Type>() { typeof(MoveCommand) }, handler.Object);

        cmd.Execute();

        Assert.Equal(5, IoC.Resolve<IDictionary<int, IHandler>>("Game.GetExceptionTree").Count);
    }
}
