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

        if (!IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("GetServrerThreads").TryGetValue(id, out ServerThread ?serverThread))
            throw new Exception();
        

        var cmd = new SoftStopServerThreadCommand(serverThread, action);

        return new SendCommand(id, new ActionCommand(() => {
            cmd.Execute();
        }));
    }
}
