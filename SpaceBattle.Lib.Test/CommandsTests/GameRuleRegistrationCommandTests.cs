using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;


public class GameRuleRegistrationCommandTests
{
    [Fact]
    public void SuccessfulGameRuleRegistrationCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Callback(() => {});

        var getStrategy = new Mock<IStrategy>();
        getStrategy.Setup(s => s.RunStrategy()).Returns(cmd.Object).Verifiable();

        var pushCommand = new Mock<ICommand>();
        pushCommand.Setup(c => c.Execute()).Callback(() => queue.Enqueue(cmd.Object)).Verifiable();

        var pushStrategy = new Mock<IStrategy>();
        pushStrategy.Setup(s => s.RunStrategy(It.IsAny<int>(), It.IsAny<ICommand>())).Returns(pushCommand.Object).Verifiable();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Thread.Command.Create.Rule.Initialization", (object[] args) => getStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.Push.ByID", (object[] args) => pushStrategy.Object.RunStrategy(args)).Execute();

        var id = 1;

        var gameRuleRegistrationCommand = new GameRuleRegistrationCommand(id);

        gameRuleRegistrationCommand.Execute();

        getStrategy.Verify();
        pushCommand.VerifyAll();
        pushStrategy.VerifyAll();
    }
}
