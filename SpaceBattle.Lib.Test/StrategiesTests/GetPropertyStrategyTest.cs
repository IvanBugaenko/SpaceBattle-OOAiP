using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class GetPropertyStrategyTests
{
    [Fact]
    public void SuccesfulGetPropertyStrategy()
    {
        var obj = new Mock<IUObject>();
        obj.Setup(o => o.getProperty("Speed")).Returns(new Vector(1, 1));

        var strategy = new GetPropertyStrategy();

        Assert.NotNull(strategy.RunStrategy(obj.Object, "Speed"));
    }
}
