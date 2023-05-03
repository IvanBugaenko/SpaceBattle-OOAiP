namespace SpaceBattle.Lib;


public class RunGameCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var queue = (Queue<ICommand>)args[0];

        return new RunGameCommandsCommand(queue);
    }
}
