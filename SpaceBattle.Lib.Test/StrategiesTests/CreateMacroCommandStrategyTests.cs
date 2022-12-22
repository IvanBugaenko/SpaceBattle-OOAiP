using Moq;

namespace SpaceBattle.Lib.Test;

public class CreateMacroCommandStrategyTests
{
    [Fact]
    public void SuccesfulCreateMacroCommandRunStrategy()
    {
        var mockIEnumCommand = new Mock<IEnumerable<ICommand>>();

        var strategy = new CreateMacroCommandStrategy();
        
        Assert.NotNull(strategy.RunStrategy(mockIEnumCommand.Object));
    }
}
