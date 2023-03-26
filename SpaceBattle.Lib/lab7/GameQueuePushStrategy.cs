namespace SpaceBattle.Lib;

public class GameQueuePushStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var queue = (Queue<ICommand>)args[0];
        var cmd = (ICommand)args[1];

        return new GameQueuePushCommand(queue, cmd);
    }
}
