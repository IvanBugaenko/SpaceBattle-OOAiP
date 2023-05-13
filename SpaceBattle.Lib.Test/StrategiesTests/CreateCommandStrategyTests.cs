using Moq;

namespace SpaceBattle.Lib.Test;


public class CreateCommandStrategyTests
{
    [Fact]
    public void SuccessfulCreateShootCommandInStrategy()
    {
        var obj = new Mock<IShootable>();

        var strategy = new CreateShootCommandStrategy();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<ShootCommand>(result);
    }

    [Fact]
    public void SuccessfulCreateRotateCommandInStrategy()
    {
        var obj = new Mock<IRotatable>();

        var strategy = new CreateRotateCommandStrategy();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<RotateCommand>(result);
    }

    [Fact]
    public void SuccessfulCreateStartMoveCommandInStrategy()
    {
        var obj = new Mock<IStartable>();

        var strategy = new CreateStartMoveCommandStrategy();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<StartCommand>(result);
    }

    [Fact]
    public void SuccessfulCreateStopMoveCommandInStrategy()
    {
        var obj = new Mock<IStopable>();

        var strategy = new CreateStopMoveCommandStrategy();

        var result = strategy.RunStrategy(obj.Object);

        Assert.NotNull(result);
        Assert.IsType<StopCommand>(result);
    }
}
