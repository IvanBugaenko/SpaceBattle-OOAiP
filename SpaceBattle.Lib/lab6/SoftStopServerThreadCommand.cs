using Hwdtech;
using System.Collections.Concurrent;
namespace SpaceBattle.Lib;

public class SoftStopServerThreadCommand: ICommand
{
    private ServerThread serverThread;
    private Action action;

    public SoftStopServerThreadCommand(int id, Action action)
    {
        ServerThread ?serverThread;

        if (IoC.Resolve<ConcurrentDictionary<int, ServerThread>>("GetServrerThreads").TryGetValue(id, out serverThread))
        {
            this.serverThread = serverThread;
        }
        else throw new Exception();

        this.action = action;
    }

    public void Execute()
    {
        if (serverThread == Thread.CurrentThread)
        {
            var cmd = new UpdateBehaviourCommand(serverThread, () => {
                serverThread.HandleCommand();
                if (serverThread.IsReceiverEmpty())
                {
                    serverThread.StopServerThread();
                    action();
                }
            });
            cmd.Execute();
        }
        else throw new Exception();
    }
}
