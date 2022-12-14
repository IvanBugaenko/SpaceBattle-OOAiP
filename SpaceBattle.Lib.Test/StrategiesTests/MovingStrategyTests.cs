using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class MovingStrategyTests
{
    [Fact]
    public void SuccesfulMovingRunStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockCommand = new Mock<SpaceBattle.Lib.ICommand>();

        var mockListCommand = new Mock<IEnumerable<SpaceBattle.Lib.ICommand>>();

        var mockStrategyReturnCommand = new Mock<IStrategy>();
        mockStrategyReturnCommand.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockCommand.Object).Verifiable();

        var mockStrategyReturnsIEnum = new Mock<IStrategy>();
        mockStrategyReturnsIEnum.Setup(x => x.RunStrategy(It.IsAny<object[]>())).Returns(mockListCommand.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.CreateMove", (object[] args) => mockStrategyReturnsIEnum.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Inject", (object[] args) => mockStrategyReturnCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Macro", (object[] args) => mockStrategyReturnCommand.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Repeat", (object[] args) => mockStrategyReturnCommand.Object.RunStrategy(args)).Execute();

        var obj = new Mock<IUObject>();

        MovingStrategy strategy = new MovingStrategy();

        strategy.RunStrategy(obj.Object);

        mockStrategyReturnCommand.VerifyAll();
        mockStrategyReturnsIEnum.VerifyAll();
    }
}
