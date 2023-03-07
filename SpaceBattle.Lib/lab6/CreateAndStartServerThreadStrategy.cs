using Hwdtech;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib;

public class CreateAndStartServerThreadStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {}; 

        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        var queue = new BlockingCollection<ICommand>();
        var sender = new ISenderAdapter(queue);
        var threadsSenders = IoC.Resolve<ConcurrentDictionary<int, ISender>>("GetServrerThreadsSenders");
        threadsSenders.TryAdd(id, sender);

        var serverThread = new ServerThread(new IReceiverAdapter(queue));
        var serverThreads = IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("GetServrerThreads");
        serverThreads.TryAdd(id, serverThread);

        return new ActionCommand(() => {
            serverThread.StartServerThread();
            action();
        });
    }
}
