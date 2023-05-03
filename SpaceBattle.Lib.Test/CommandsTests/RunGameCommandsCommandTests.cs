using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;


public class RunGameCommandsCommandTests
{
    [Fact]
    public void SuccessfulRunGameCommandsCommandExecute()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
        
        var queue = new Queue<ICommand>();

        queue.Enqueue(new ActionCommand(() => {
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", scope)).Execute();
            
            var strategy = new Mock<IStrategy>();
            strategy.Setup(s => s.RunStrategy()).Returns(0.0);

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetQuantumOfTime", (object[] args) => strategy.Object.RunStrategy(args)).Execute();  
        }));

        queue.Enqueue(new ActionCommand(() => {}));
        queue.Enqueue(new ActionCommand(() => {}));

        var getQuantumOfTimeStrategy = new Mock<IStrategy>();
        getQuantumOfTimeStrategy.Setup(s => s.RunStrategy()).Returns(300.0);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.GetCommand", (object[] args) => queue.Dequeue()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetQuantumOfTime", (object[] args) => getQuantumOfTimeStrategy.Object.RunStrategy(args)).Execute();

        var runGameCommandsCommand = new RunGameCommandsCommand(queue);

        Assert.Equal(300.0, IoC.Resolve<double>("Game.GetQuantumOfTime"));

        runGameCommandsCommand.Execute();

        Assert.Equal(0.0, IoC.Resolve<double>("Game.GetQuantumOfTime"));
        Assert.True(queue.Count == 2);
    }

    [Fact]
    public void UnsuccessfulRunGameCommandsCommandExecuteWithFindExceptionHandler()
    {
        var handler = new Mock<IHandler>();
        handler.Setup(h => h.Handle()).Callback(() => {}).Verifiable();

        new InitScopeBasedIoCImplementationCommand().Execute();
        var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", scope).Execute();
        
        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Throws<Exception>();

        queue.Enqueue(cmd.Object);
        queue.Enqueue(new ActionCommand(() => {
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", scope)).Execute();
            
            var strategy = new Mock<IStrategy>();
            strategy.Setup(s => s.RunStrategy()).Returns(0.0);

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetQuantumOfTime", (object[] args) => strategy.Object.RunStrategy(args)).Execute();  
        }));

        var getQuantumOfTimeStrategy = new Mock<IStrategy>();
        getQuantumOfTimeStrategy.Setup(s => s.RunStrategy()).Returns(300.0);

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.GetCommand", (object[] args) => queue.Dequeue()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetExceptionHandler", (object[] args) => handler.Object).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetQuantumOfTime", (object[] args) => getQuantumOfTimeStrategy.Object.RunStrategy(args)).Execute();

        var runGameCommandsCommand = new RunGameCommandsCommand(queue);

        runGameCommandsCommand.Execute();

        handler.VerifyAll();
    }
}
