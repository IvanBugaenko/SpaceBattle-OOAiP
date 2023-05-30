using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;


public class ShootCommandTests
{
    [Fact]
    public void SuccessfulShootCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Callback(() => {});

        var pushCommand = new Mock<ICommand>();
        pushCommand.Setup(c => c.Execute()).Callback(() => queue.Enqueue(cmd.Object)).Verifiable();

        var queueStrategy = new Mock<IStrategy>();
        queueStrategy.Setup(s => s.RunStrategy()).Returns(queue).Verifiable();

        var shootStrategy = new Mock<IStrategy>();
        shootStrategy.Setup(s => s.RunStrategy(It.IsAny<object[]>())).Returns(cmd.Object).Verifiable();

        var pushStrategy = new Mock<IStrategy>();
        pushStrategy.Setup(s => s.RunStrategy(It.IsAny<Queue<ICommand>>(), It.IsAny<ICommand>())).Returns(pushCommand.Object).Verifiable();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Get", (object[] args) => queueStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.IUObject.Shoot", (object[] args) => shootStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push", (object[] args) => pushStrategy.Object.RunStrategy(args)).Execute();

        var obj = new Mock<IShootable>();
        
        var shootCommand = new ShootCommand(obj.Object);

        shootCommand.Execute();

        Assert.True(queue.Count() == 1);
        pushCommand.VerifyAll();
        queueStrategy.VerifyAll();
        shootStrategy.Verify();
        pushStrategy.VerifyAll();
    }
}
