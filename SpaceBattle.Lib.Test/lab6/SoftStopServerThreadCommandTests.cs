using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib.Test;

public class SoftStopServerThreadCommandTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public SoftStopServerThreadCommandTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();
    }

    [Fact]
    public void UnsuccessfulSoftStopServerThreadCommandBecauseExecuteThrowExceptionWithDifferentThreads()
    {
        var key = 1;

        var ssFlag = false;

        var are = new AutoResetEvent(true);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();

        var c = (ICommand)createAndStartSTStrategy.RunStrategy(key, () =>
        {
            are.WaitOne();
        });
        c.Execute();

        var serverThread = mapServerThreads[key];
        var ss = new SoftStopServerThreadCommand(serverThread, () => { 
            ssFlag = true; 
        });

        Assert.Throws<Exception>(() => ss.Execute());
        Assert.False(ssFlag);

        var hardStopStrategy = new HardStopServerThreadStrategy();
        var hs = (ICommand)hardStopStrategy.RunStrategy(key);
        hs.Execute();
    }

    [Fact]
    public void SuccessfulSoftStopCommandExecuteWithOtherCommandsInServerThreadSender()
    {
        var key = 4;
        var isExecute = false;

        var are = new AutoResetEvent(true);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.RunStrategy(key, () =>
        {
            are.WaitOne();
        });
        c.Execute();
        
        var softStopStrategy = new SoftStopServerThreadStrategy();
        var sendStrategy = new SendCommandStrategy();

        var ss = (ICommand)softStopStrategy.RunStrategy(key);
        ss.Execute();

        var c2 = (ICommand)sendStrategy.RunStrategy(key, new ActionCommand(() =>
        {
            isExecute = true;
            are.WaitOne();
        }));
        c2.Execute();
        are.Set();

        Thread.Sleep(1000);

        Assert.True(isExecute);
    }
}
