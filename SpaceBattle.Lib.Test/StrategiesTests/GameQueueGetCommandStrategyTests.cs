using Moq;

namespace SpaceBattle.Lib.Test;


public class GameQueueGetCommandStrategyTests
{
    [Fact]
    public void SuccessfulGameQueueGetCommand()
    {
        var cmd = new Mock<ICommand>();
        var queue = new Queue<ICommand>();

        queue.Enqueue(cmd.Object);

        var gameQueueGetCommandStrategy = new GameQueueGetCommandStrategy();

        var result = gameQueueGetCommandStrategy.RunStrategy(queue);

        Assert.True(queue.Count() == 0);
        Assert.True(result == cmd.Object);
    }

    [Fact]
    public void UnsuccessfulGameQueueGetCommandBecauseQueueIsEmpty()
    {
        var cmd = new Mock<ICommand>();
        var queue = new Queue<ICommand>();

        var gameQueueGetCommandStrategy = new GameQueueGetCommandStrategy();

        Assert.Throws<Exception>(() => gameQueueGetCommandStrategy.RunStrategy(queue));
    }
}
