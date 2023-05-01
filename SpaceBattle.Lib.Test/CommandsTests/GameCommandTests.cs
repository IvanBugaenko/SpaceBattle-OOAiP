using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;


public class GameCommandTests
{
    [Fact]
    public void SuccessfulGameCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Callback(() => {}).Verifiable();

        var runCommandsStrategy = new Mock<IStrategy>();
        runCommandsStrategy.Setup(s => s.RunStrategy(It.IsAny<Queue<ICommand>>(), It.IsAny<double>())).Returns(cmd.Object).Verifiable();

        var getQuantumOfTimeStrategy = new Mock<IStrategy>();
        getQuantumOfTimeStrategy.Setup(s => s.RunStrategy()).Returns(1.1).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.RunCommands", (object[] args) => runCommandsStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetQuantumOfTime", (object[] args) => getQuantumOfTimeStrategy.Object.RunStrategy(args)).Execute();

        var game = new GameCommand(queue, scope);

        var scopeNew = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scopeNew).Execute();

        game.Execute();

        Assert.True(scope == IoC.Resolve<object>("Scopes.Current"));
        cmd.VerifyAll();
        runCommandsStrategy.VerifyAll();
        getQuantumOfTimeStrategy.VerifyAll();
    }
}
