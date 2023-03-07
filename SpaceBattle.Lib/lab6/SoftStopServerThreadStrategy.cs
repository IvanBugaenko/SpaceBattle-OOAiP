namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class SoftStopServerThreadStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {}; 

        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        ServerThread ?serverThread;
        var serverThreads = IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("GetServrerThreads");
        if(!serverThreads.TryGetValue(id, out serverThread)) throw new Exception();

        ISender ?sender;
        var threadsSenders = IoC.Resolve<ConcurrentDictionary<int, ISender>>("GetServrerThreadsSenders");
        if(!threadsSenders.TryGetValue(id, out sender)) throw new Exception();

        var cmd = new ServerThreadSoftStopCommand(serverThread, sender, action);

        return new UpdateBehaviourCommand(serverThread, () => {
            serverThread.HandleCommand();
            cmd.Execute();
        });
    }
}