namespace SpaceBattle.Lib;


public class GameRuleRegistrationStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var gameID = (int)args[0];

        return new GameRuleRegistrationCommand(gameID);
    }
}
