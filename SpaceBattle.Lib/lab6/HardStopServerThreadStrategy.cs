using Hwdtech;

namespace SpaceBattle.Lib;
using System.Collections.Concurrent;

public class HardStopServerThreadStrategy : IStrategy
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

        var cmd = new ServerThreadHardStopCommand(serverThread);

        return new SendCommand(id, new ActionCommand(() => 
        {
            cmd.Execute();
            action();
        }));
    }
}
