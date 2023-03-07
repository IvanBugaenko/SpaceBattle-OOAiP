namespace SpaceBattle.Lib;

public class ServerThreadSoftStopCommand: ICommand
{
    private ServerThread serverThread;
    private ISender sender;
    private Action action;

    public ServerThreadSoftStopCommand(ServerThread serverThread, ISender sender, Action action)
    {
        this.serverThread = serverThread;
        this.sender = sender;
        this.action = action;
    }

    public void Execute()
    {
        if (serverThread == Thread.CurrentThread && sender.isEmpty())
        {
            serverThread.StopServerThread();
            action();
        }
    }
}
