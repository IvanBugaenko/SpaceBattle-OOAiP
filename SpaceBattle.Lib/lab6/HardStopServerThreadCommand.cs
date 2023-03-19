using Hwdtech;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib;

public class HardStopServerThreadCommand: ICommand
{
    private ServerThread serverThread;

    public HardStopServerThreadCommand(int id)
    {
        ServerThread ?serverThread;

        if (IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("GetServrerThreads").TryGetValue(id, out serverThread))
        {
            this.serverThread = serverThread;
        }
        else throw new Exception();
    }

    public void Execute()
    {
        var t = Thread.CurrentThread;
        if (serverThread == t)
        {
            serverThread.StopServerThread();
        }
        else
        {
            throw new Exception();
        }
        serverThread.StopServerThread();
    }
}
