using Hwdtech;
using Hwdtech.Ioc;
using System.Collections.Concurrent;
using Moq;

namespace SpaceBattle.Lib.Test;

public class ServerThreadTests
{
    ConcurrentDictionary<int, ServerThread> mapServerThreads = new ConcurrentDictionary<int, ServerThread>();
    ConcurrentDictionary<int, ISender> mapServerThreadsSenders = new ConcurrentDictionary<int, ISender>();

    public ServerThreadTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreads", (object[] args) => mapServerThreads).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetServrerThreadsSenders", (object[] args) => mapServerThreadsSenders).Execute();;
    }

    [Fact]
    public void SuccessfulCreateStartAndHardStopServerThreadWithoutParamsForStrategies()
    {
        var isActive = false;

        var key = 1;

        var are = new AutoResetEvent(false);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.RunStrategy(key);
        c.Execute();

        var sendStrategy = new SendCommandStrategy();
        var c1 = (ICommand)sendStrategy.RunStrategy(key, new ActionCommand(() =>
        {
            isActive = true;
            are.Set();
        }));
        c1.Execute();

        are.WaitOne();

        Assert.True(isActive);
        Assert.True(mapServerThreads.TryGetValue(key, out ServerThread? st));
        Assert.True(mapServerThreadsSenders.TryGetValue(key, out ISender? s));

        var hardStopStrategy = new HardStopServerThreadStrategy();
        var hs = (ICommand)hardStopStrategy.RunStrategy(key);
        hs.Execute();
    }

    [Fact]
    public void SuccessfulCreateStartAndHardStopServerThreadWithParamsForStrategies()
    {
        var isActive = false;
        var createAndStartFlag = false;
        var hsFlag = false;

        var key = 2;

        var are = new AutoResetEvent(false);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.RunStrategy(key, () =>
        {
            createAndStartFlag = true;
        });
        c.Execute();

        var sendStrategy = new SendCommandStrategy();
        var c1 = (ICommand)sendStrategy.RunStrategy(key, new ActionCommand(() =>
        {
            isActive = true;
            are.Set();
        }));
        c1.Execute();

        are.WaitOne();

        Assert.True(isActive);
        Assert.True(mapServerThreads.TryGetValue(key, out ServerThread? st));
        Assert.True(mapServerThreadsSenders.TryGetValue(key, out ISender? s));
        Assert.True(createAndStartFlag);

        var hardStopStrategy = new HardStopServerThreadStrategy();
        var hs = (ICommand)hardStopStrategy.RunStrategy(key, () =>
        {
            hsFlag = true;
            are.Set();
        });
        hs.Execute();
        are.WaitOne();

        Assert.True(hsFlag);
    }

    [Fact]
    public void ServerThreadGetHashCodeTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new IReceiverAdapter(queue1));
        var queue2 = new BlockingCollection<ICommand>();
        var serverThread2 = new ServerThread(new IReceiverAdapter(queue2));
        Assert.True(serverThread1.GetHashCode() != serverThread2.GetHashCode());
    }

    [Fact]
    public void ServerThreadEqualsIsNotThreadTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new IReceiverAdapter(queue1));
        Assert.False(serverThread1.Equals(2));
    }

    [Fact]
    public void ServerThreadEqualsTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new IReceiverAdapter(queue1));
        Assert.False(serverThread1.Equals(Thread.CurrentThread));
    }

    [Fact]
    public void ServerThreadOperatorEqualsTest()
    {
        var queue1 = new BlockingCollection<ICommand>();
        var serverThread1 = new ServerThread(new IReceiverAdapter(queue1));
        Assert.True(serverThread1 != Thread.CurrentThread);
    }

    [Fact]
    public void FindHandlerExceptionForServerThread()
    {
        var handleFlag = false;

        var key = 11;
        var are = new AutoResetEvent(false);

        var createAndStartSTStrategy = new CreateAndStartServerThreadStrategy();
        var c = (ICommand)createAndStartSTStrategy.RunStrategy(key);
        c.Execute();

        var sendStrategy = new SendCommandStrategy();

        var c1 = (ICommand)sendStrategy.RunStrategy(key, new ActionCommand(() =>
        {
            new InitScopeBasedIoCImplementationCommand().Execute();
            IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

            var handler = new Mock<IHandler>();
            handler.Setup(c => c.Handle()).Callback(() => are.Set());

            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "GetExceptionHandler", (object[] args) => handler.Object).Execute();

            handleFlag = true;
            throw new Exception();
        }));

        c1.Execute();

        are.WaitOne();

        Assert.True(handleFlag);

        var hardStopStrategy = new HardStopServerThreadStrategy();

        var hs = (ICommand)hardStopStrategy.RunStrategy(key);

        hs.Execute();
    }
}
