using Moq;

namespace SpaceBattle.Lib.Test;


public class CreateAdapterBuilderStrategyTests
{
    [Fact]
    public void SuccessfulCreateAdapterBuilderStrategyRunStrategy()
    {
        var strategy = new CreateAdapterBuilderStrategy();

        var result = (IBuilder)strategy.RunStrategy(typeof(IUObject), typeof(IMovable));

        Assert.IsType<AdapterBuilder>(result);
        Assert.NotNull(result);
    }
}
