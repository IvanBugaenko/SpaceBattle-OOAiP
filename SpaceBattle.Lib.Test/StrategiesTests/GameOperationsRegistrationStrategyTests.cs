namespace SpaceBattle.Lib.Test;


public class GameOperationsRegistrationStrategyTest
{
    [Fact]
    public void SuccessfulGameOperationsRegistrationStrategyRunStrategy()
    {
        var id = 1;

        var strategy = new GameOperationsRegistrationStrategy();

        var result = strategy.RunStrategy(id);

        Assert.NotNull(result);
        Assert.IsType<GameOperationsRegistrationCommand>(result);
    }
}
