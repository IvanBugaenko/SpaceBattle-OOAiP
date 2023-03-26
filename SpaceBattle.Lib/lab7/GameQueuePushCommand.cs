namespace SpaceBattle.Lib;

public class GameQueuePushCommand: ICommand
{
    Queue<ICommand> queue;
    ICommand cmd;

    public GameQueuePushCommand(Queue<ICommand> queue, ICommand cmd)
    {
        this.queue = queue;
        this.cmd = cmd;
    }

    public void Execute()
    {
        queue.Enqueue(cmd);
    }
}
