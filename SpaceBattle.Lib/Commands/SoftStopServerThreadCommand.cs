using Hwdtech;
using System.Collections.Concurrent;
namespace SpaceBattle.Lib;

public class SoftStopServerThreadCommand: ICommand
{
    private ServerThread serverThread;
    private Action action;

    public SoftStopServerThreadCommand(ServerThread serverThread, Action action)
    {
        this.serverThread = serverThread;

        this.action = action;
    }

    public void Execute()
    {
        if (this.serverThread == Thread.CurrentThread)
        {
            var cmd = new UpdateBehaviourCommand(this.serverThread, () => {
                if (this.serverThread.IsReceiverEmpty())
                {
                    this.serverThread.StopServerThread();
                    action();
                }
                else this.serverThread.HandleCommand();
            });
            cmd.Execute();
        }
        else throw new Exception();
    }
}
