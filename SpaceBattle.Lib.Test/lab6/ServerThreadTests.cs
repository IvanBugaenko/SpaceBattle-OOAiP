using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;
using Moq;

namespace SpaceBattle.Lib.Test;

public class ServerThreadTests
{
    Mock<ICommand> emptyCmd = new Mock<ICommand>();
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public ServerThreadTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();

        emptyCmd.Setup(c => c.Execute()).Callback(() => {});
    }

    [Fact]
    public void SuccessfulCreateAndStartServerThread()
    {
        bool succStart = false;

        var createAndStartServerThreadStrategy = new CreateAndStartServerThreadStrategy();

        var key = 1;

        var c = (ICommand)createAndStartServerThreadStrategy.RunStrategy(key, () => {
            succStart = true;
            new AutoResetEvent(true).WaitOne();
        });

        c.Execute();

        Assert.True(succStart);
        Assert.True(mapServerThreads.TryGetValue(key, out ServerThread? st));
        Assert.True(mapServerThreadsSenders.TryGetValue(key, out ISender? s));
    }
}
