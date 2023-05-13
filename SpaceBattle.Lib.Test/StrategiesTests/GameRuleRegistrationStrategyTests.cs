namespace SpaceBattle.Lib.Test;


public class GameRuleRegistrationStrategyTest
{
    [Fact]
    public void SuccessfulGameRuleRegistrationStrategyRunStrategy()
    {
        var id = 1;

        var strategy = new GameRuleRegistrationStrategy();

        var result = strategy.RunStrategy(id);

        Assert.NotNull(result);
        Assert.IsType<GameRuleRegistrationCommand>(result);
    }
}
