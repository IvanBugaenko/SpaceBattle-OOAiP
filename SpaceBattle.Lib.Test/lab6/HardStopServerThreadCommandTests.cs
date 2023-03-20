using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class HardStopServerThreadCommandTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public HardStopServerThreadCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void UnsuccessfulHardStopServerThreadCommandThrowsExceptionFromExecute()
    {
        var key = 5;
        var are = new AutoResetEvent(true);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.RunStrategy(key, ()=> { 
            are.WaitOne();
        });
        c.Execute();

        var serverThread = mapServerThreads[key];
        var hs = new HardStopServerThreadCommand(serverThread);

        Assert.Throws<Exception>(() => hs.Execute());

        var hardStopStrategy = new HardStopServerThreadStrategy();

        var hs2 = (ICommand)hardStopStrategy.RunStrategy(key);

        hs2.Execute();
    }
}
