namespace SpaceBattle.Lib;


public class RunGameCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var queue = (Queue<ICommand>)args[0];
        var quantum = (double)args[1];

        return new RunGameCommandsCommand(queue, quantum);
    }
}
