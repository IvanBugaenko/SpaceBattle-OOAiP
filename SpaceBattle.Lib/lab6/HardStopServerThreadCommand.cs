using Hwdtech;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib;

public class ServerThreadHardStopCommand: ICommand
{
    private ServerThread serverThread;

    public ServerThreadHardStopCommand(int id)
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
        if (serverThread == Thread.CurrentThread)
        {
            serverThread.StopServerThread();
        }
        else
        {
            throw new Exception();
        }
    }
}

