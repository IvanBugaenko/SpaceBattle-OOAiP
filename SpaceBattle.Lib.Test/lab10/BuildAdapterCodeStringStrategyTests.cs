using Hwdtech;
using Hwdtech.Ioc;
using System.Reflection;

using Moq;

namespace SpaceBattle.Lib.Test;


public class BuildAdapterCodeStringStrategyTests
{
    [Fact]
    public void SuccessfulBuildAdapterCodeStringRunStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var builder = new Mock<IBuilder>();
        builder.Setup(b => b.Build()).Returns("1").Verifiable();
        builder.Setup(b => b.AddProperty(It.IsAny<object>())).Callback(() => {}).Verifiable();

        var builderStrategy = new Mock<IStrategy>();
        builderStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(builder.Object).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Options.Builder.Adapter", (object[] args) => builderStrategy.Object.RunStrategy(args)).Execute();

        var strategy = new BuildAdapterCodeStringStrategy();
        
        var result = (string)strategy.RunStrategy(typeof(IUObject), typeof(IMovable));

        Assert.Equal("1", result);
        builder.VerifyAll();
        builderStrategy.VerifyAll();
    }
}
