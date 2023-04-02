using Moq;

namespace SpaceBattle.Lib.Test;

public class GameQueuePushStrategyTests
{
    [Fact]
    public void SuccessfulGameQueuePushStrategyRunStrategy()
    {
        var gameQueuePushStrategy = new GameQueuePushStrategy();

        Assert.NotNull(gameQueuePushStrategy.RunStrategy(1, new Mock<ICommand>().Object));
    }
}
