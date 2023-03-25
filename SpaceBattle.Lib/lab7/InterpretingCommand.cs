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
        
    }
}