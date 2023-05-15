using Moq;
using Hwdtech;
using Hwdtech.Ioc;


namespace SpaceBattle.Lib.Test;


public class CreateCommandStrategyTests
{
    [Fact]
    public void SuccessfulCreateShootCommandInStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var shootableObj = new Mock<IShootable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(shootableObj.Object);
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Adapter.Create", (object[] args) => adapterStrategy.Object.RunStrategy(args)).Execute();
        
        var strategy = new CreateShootCommandStrategy();

        var obj = new Mock<IUObject>();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<ShootCommand>(result);
    }

    [Fact]
    public void SuccessfulCreateRotateCommandInStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var rotatableObj = new Mock<IRotatable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(rotatableObj.Object);
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Adapter.Create", (object[] args) => adapterStrategy.Object.RunStrategy(args)).Execute();
        
        var strategy = new CreateRotateCommandStrategy();

        var obj = new Mock<IUObject>();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<RotateCommand>(result);
    }

    [Fact]
    public void SuccessfulCreateStartMoveCommandInStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var startMoveObj = new Mock<IStartable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(startMoveObj.Object);
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Adapter.Create", (object[] args) => adapterStrategy.Object.RunStrategy(args)).Execute();
        
        var strategy = new CreateStartMoveCommandStrategy();

        var obj = new Mock<IUObject>();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<StartCommand>(result);
    }

    [Fact]
    public void SuccessfulCreateStopMoveCommandInStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var stopMoveObj = new Mock<IStopable>();

        var adapterStrategy = new Mock<IStrategy>();
        adapterStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(stopMoveObj.Object);
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Adapter.Create", (object[] args) => adapterStrategy.Object.RunStrategy(args)).Execute();
        
        var strategy = new CreateStopMoveCommandStrategy();

        var obj = new Mock<IUObject>();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<StopCommand>(result);
    }
}
