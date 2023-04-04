namespace SpaceBattle.Lib;

public class GameQueuePushStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var cmd = (ICommand)args[1];

        return new GameQueuePushCommand(id, cmd);
    }
}
