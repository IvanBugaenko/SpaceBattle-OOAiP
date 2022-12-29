using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class PrepareDataToColisionTests
{

    [Fact]
    public void SuccesfulPrepareDataToCollisionStrategyTest()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        List<int> property1 = new List<int>(){12, 32, 56, 4};
        List<int> property2 = new List<int>(){12, 32, 56, 4};

        IStrategy PrepareData = new PrepareDataToCollisionStrategy();

        Object.Equals(new List<int>(){0, 0, 0, 0}, PrepareData.RunStrategy(property1,property2));
    }
}
