using Hwdtech;
namespace SpaceBattle.Lib;

public class InterpretingCommand: ICommand
{
    IMessage message;

    public InterpretingCommand(IMessage message)
    {
        this.message = message;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>("Game.CreateCommand", message);

        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<Queue<ICommand>>("Game.Queue"), cmd).Execute();
    }
}
