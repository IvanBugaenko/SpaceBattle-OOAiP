namespace SpaceBattle.Lib;

public class ServerThread
{
    private Thread thread;
    private Action strategy;
    private bool stop = false;

    private IReciver queue;

    public ServerThread(IReciver queue)
    {
        this.queue = queue;

        strategy = () => 
        {
            HandleCommand();
        };

        this.thread = new Thread(() =>
        {
            while (!stop) strategy();
        });
    }

    public Thread GetInnerThread()
    {
        return this.thread;
    }

    internal void HandleCommand()
    {
        try 
        {
            this.queue.Recieve().Execute();
        }
        catch
        {
            throw new Exception();
        }
    }

    internal void UpdateBehaviour(Action newBehaviour)
    {
        this.strategy = newBehaviour;
    }

    internal void StopServerThread()
    {
        this.stop = true;
    }

    public void StartServerThread()
    {
        thread.Start();
    }
}
