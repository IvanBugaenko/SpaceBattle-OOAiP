using Hwdtech;

namespace SpaceBattle.Lib;

public class ServerThread
{
    private Thread thread;
    private Action strategy;
    private bool stop = false;
    private IReceiver queue;

    public ServerThread(IReceiver queue)
    {
        this.queue = queue;

        strategy = () => 
        {
            HandleCommand();
        };

        this.thread = new Thread(() =>
        {
            while (!stop) strategy(); // -> () => {HandleCommand(); SoftStop.Execute()}
        });
    }

    internal void HandleCommand()
    {
        var cmd = this.queue.Receive();
        try 
        {
            cmd.Execute();
        }
        catch (Exception e)
        {
            IoC.Resolve<IHandler>("GetExceptionHandler", new List<Type>{cmd.GetType(), e.GetType()});
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

    public override int GetHashCode()
    {
        return this.thread.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Thread th && th == this.thread;
    }

    public static bool operator ==(ServerThread st, Thread th)
    {
        return st.thread == th;
    }

    public static bool operator !=(ServerThread st, Thread th)
    {
        return !(st == th);
    }
}
