using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class HardStopServerThreadStrategyTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public HardStopServerThreadStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadStrategyThrowsExceptionBecauseFalseKeyInRunStratehyCondition()
    {
        var key = 1;
        var falseKey = 2;

        var are = new AutoResetEvent(true);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.RunStrategy(key, () =>
        {
            are.WaitOne();
        });
        c.Execute();

        var hardStopStrategy = new HardStopServerThreadStrategy();

        Assert.Throws<Exception>(() =>
        {
            var hs = (ICommand)hardStopStrategy.RunStrategy(falseKey);
        });

        var hs = (ICommand)hardStopStrategy.RunStrategy(key);
        hs.Execute();
    }
}
