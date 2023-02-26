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
        if (Thread.CurrentThread == serverThread.GetInnerThread())
        {
            serverThread.StopServerThread();
        }
        else
        {
            throw new Exception();
        }
    }
}
