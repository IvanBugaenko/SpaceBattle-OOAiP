using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class GameCreateCommandStrategyTests
{
    [Fact]
    public void SuccessfulGameCreateCommandStrategyRunStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromUObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.OrderType).Returns("Test").Verifiable();
        message.Setup(m => m.GameItemID).Returns(1).Verifiable();
        message.Setup(m => m.Properties).Returns(new Dictionary<string, object>(){
            {"dlfkldkf", 3}
        }).Verifiable();

        var strstegy = new GameCreateCommandStrategy();
        var c = strstegy.RunStrategy(message.Object);

        message.VerifyAll();
        createCommandStrategy.VerifyAll();
        setPropsCommandStrategy.VerifyAll();
        getUObjectFromUObjectMapStrtegy.VerifyAll();
        Assert.NotNull(c);
    }

    [Fact]
    public void UnuccessfulGameCreateCommandStrategyRunStrategyThrowExceptionOnOrderTypeFromMessage()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromUObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.OrderType).Throws<Exception>();

        var strstegy = new GameCreateCommandStrategy();

        Assert.Throws<Exception>(() => strstegy.RunStrategy(message.Object));
    }

    [Fact]
    public void UnuccessfulGameCreateCommandStrategyRunStrategyThrowExceptionOnOGameItemIDFromMessage()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromUObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.GameItemID).Throws<Exception>();

        var strstegy = new GameCreateCommandStrategy();

        Assert.Throws<Exception>(() => strstegy.RunStrategy(message.Object));
    }

    [Fact]
    public void UnuccessfulGameCreateCommandStrategyRunStrategyThrowExceptionOnPropertiesFromMessage()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var obj = new Mock<IUObject>();

        var getUObjectFromUObjectMapStrtegy = new Mock<IStrategy>();
        getUObjectFromUObjectMapStrtegy.Setup(s => s.RunStrategy(It.IsAny<int>())).Returns(obj.Object).Verifiable();

        var setPropsCommand = new Mock<ICommand>();
        setPropsCommand.Setup(c => c.Execute()).Callback(() => {});

        var setPropsCommandStrategy = new Mock<IStrategy>();
        setPropsCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(setPropsCommand.Object).Verifiable();

        var cmd = new Mock<ICommand>();

        var createCommandStrategy = new Mock<IStrategy>();
        createCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<IUObject>())).Returns(cmd.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetUObjectFromUObjectMap", (object[] args) => getUObjectFromUObjectMapStrtegy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameUObjectSetProperty", (object[] args) => setPropsCommandStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "CreateCommand.Test", (object[] args) => createCommandStrategy.Object.RunStrategy(args)).Execute();

        var message = new Mock<IMessage>();
        message.Setup(m => m.Properties).Throws<Exception>();

        var strstegy = new GameCreateCommandStrategy();

        Assert.Throws<Exception>(() => strstegy.RunStrategy(message.Object));
    }
}
