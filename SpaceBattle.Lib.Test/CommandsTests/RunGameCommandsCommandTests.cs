using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;


public class RunGameCommandsCommandTests
{
    [Fact]
    public void SuccessfulRunGameCommandsCommandExecute()
    {
        var queue = new Queue<ICommand>();

        queue.Enqueue(new ActionCommand(() => Thread.Sleep(500)));
        queue.Enqueue(new ActionCommand(() => Thread.Sleep(500)));
        queue.Enqueue(new ActionCommand(() => Thread.Sleep(500)));

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.GetCommand", (object[] args) => queue.Dequeue()).Execute();

        var quantum = 900.0;

        var runGameCommandsCommand = new RunGameCommandsCommand(queue, quantum);

        runGameCommandsCommand.Execute();

        Assert.Single(queue);
    }

    [Fact]
    public void UnsuccessfulRunGameCommandsCommandExecuteWithFindExceptionHandler()
    {
        var queue = new Queue<ICommand>();

        var cmd = new Mock<ICommand>();
        cmd.Setup(c => c.Execute()).Throws<Exception>();

        queue.Enqueue(cmd.Object);
        queue.Enqueue(new ActionCommand(() => Thread.Sleep(500)));

        var handler = new Mock<IHandler>();
        handler.Setup(h => h.Handle()).Callback(() => {}).Verifiable();

        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Queue.GetCommand", (object[] args) => queue.Dequeue()).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetExceptionHandler", (object[] args) => handler.Object).Execute();

        var quantum = 200;

        var runGameCommandsCommand = new RunGameCommandsCommand(queue, quantum);

        runGameCommandsCommand.Execute();

        // Assert.Throws<Exception>(() => );
        handler.VerifyAll();
    }
}
