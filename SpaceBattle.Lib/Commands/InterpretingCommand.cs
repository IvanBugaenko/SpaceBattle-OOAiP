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

        var id = message.GameID;

        IoC.Resolve<ICommand>("Game.Queue.Push", id, cmd).Execute();
    }
}
