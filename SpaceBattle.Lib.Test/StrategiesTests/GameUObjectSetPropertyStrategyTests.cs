using Moq;

namespace SpaceBattle.Lib.Test;

public class GameUObjectSetPropertyStrategyTests
{
    [Fact]
    public void SuccessfulGameUObjectSetPropertyStrategyRunStrategy()
    {
        var gameUObjectSetPropertyStrategy = new GameUObjectSetPropertyStrategy();

        Assert.NotNull(gameUObjectSetPropertyStrategy.RunStrategy(new Mock<IUObject>().Object, "ldjkgkdg", 1));
    }
}
