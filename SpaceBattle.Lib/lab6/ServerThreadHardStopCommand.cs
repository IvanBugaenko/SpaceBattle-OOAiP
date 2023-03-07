namespace SpaceBattle.Lib;

public class ServerThreadHardStopCommand: ICommand
{
    ServerThread serverThread;

    public ServerThreadHardStopCommand(ServerThread serverThread)
    {
        this.serverThread = serverThread;
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

