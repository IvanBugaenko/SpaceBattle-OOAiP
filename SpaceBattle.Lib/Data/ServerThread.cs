namespace SpaceBattle.Lib;

public class ServerThread
{
    private Thread thread;
    private Action strategy;
    private bool stop;

    public ServerThread()
    {
        strategy = () => 
        {
            HandleCommand();
        };

        this.thread = new Thread(() =>
        {
            while (!stop) strategy();
        });
    }

    internal void HandleCommand()
    {

    }


}