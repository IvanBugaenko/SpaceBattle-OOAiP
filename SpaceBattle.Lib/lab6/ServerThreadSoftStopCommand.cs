namespace SpaceBattle.Lib;

public class ServerThreadSoftStopCommand: ICommand
{
    ServerThread serverThread;

    public ServerThreadSoftStopCommand(ServerThread serverThread)
    {
        this.serverThread = serverThread;
    }

    public void Execute()
    {
        
    }
}
