using Hwdtech;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib;

public class CreateAndStartServerThreadCommand: ICommand
{
    private int id;

    public CreateAndStartServerThreadCommand(int id)
    {
        this.id = id;
    }

    public void Execute()
    {
        var queue = new BlockingCollection<ICommand>();
        
        var sender = new ISenderAdapter(queue);
        var threadsSenders = IoC.Resolve<ConcurrentDictionary<int, ISender>>("GetServrerThreadsSenders");
        threadsSenders.TryAdd(this.id, sender);

        var serverThread = new ServerThread(new IReceiverAdapter(queue));
        var serverThreads = IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("GetServrerThreads");
        serverThreads.TryAdd(this.id, serverThread);

        serverThread.StartServerThread();
    }
}
