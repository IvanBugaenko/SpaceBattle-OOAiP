using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;

public class GetGameQueueStartegyTests
{
    Dictionary<int, Queue<ICommand>> queueMap = new Dictionary<int, Queue<ICommand>>(){
        {1, new Queue<ICommand>()}
    };

    public GetGameQueueStartegyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GameQueues", (object[] args) => this.queueMap).Execute();
    }

    [Fact]
    public void SuccessfulGetGameQueueStartegyRunStrategy()
    {
        var id = 1;

        var strategy = new GetGameQueueStartegy();

        var q = strategy.RunStrategy(id);

        Assert.Equal(this.queueMap[id], q);
    }

    [Fact]
    public void UnsuccessfulGetGameQueueStartegyRunStrategyBecauseCannotFindKeyInGameQueueMap()
    {
        var falseID = 2;

        var strategy = new GetGameQueueStartegy();

        Assert.Throws<Exception>(() => strategy.RunStrategy(falseID));
    }
}
