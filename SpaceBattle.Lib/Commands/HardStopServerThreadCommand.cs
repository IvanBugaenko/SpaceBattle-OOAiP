using Hwdtech;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib;

public class HardStopServerThreadCommand: ICommand
{
    private ServerThread serverThread;

    public HardStopServerThreadCommand(ServerThread serverThread)
    {
        this.serverThread = serverThread;
    }

    public void Execute()
    {
        if (this.serverThread == Thread.CurrentThread)
        {
            this.serverThread.StopServerThread();
        }
        else
        {
            throw new Exception();
        }
        this.serverThread.StopServerThread();
    }
}
