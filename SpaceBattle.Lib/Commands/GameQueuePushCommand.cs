using Hwdtech;
namespace SpaceBattle.Lib;

public class GameQueuePushCommand: ICommand
{
    int id;
    ICommand cmd;

    public GameQueuePushCommand(int id, ICommand cmd)
    {
        this.id = id;
        this.cmd = cmd;
    }

    public void Execute()
    {
        var queue = IoC.Resolve<Queue<ICommand>>("GetGameQueueByID", this.id);
        queue.Enqueue(cmd);
    }
}
