using Moq;

namespace SpaceBattle.Lib.Test;


public class RunGameCommandStrategyTests
{
    [Fact]
    public void SuccessfulRunGameCommandStrategyReturnRunGameCommandsCommand()
    {
        var queue = new Queue<ICommand>();
        var quantum = 1.1;

        var strategy = new RunGameCommandStrategy();

        Assert.NotNull(strategy.RunStrategy(queue, quantum));
    }
}
