namespace SpaceBattle.Lib;


public class GameOperationsRegistrationStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var gameID = (int)args[0];

        return new GameOperationsRegistrationCommand(gameID);
    }
}
