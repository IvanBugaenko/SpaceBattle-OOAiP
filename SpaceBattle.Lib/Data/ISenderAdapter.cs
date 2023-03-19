namespace SpaceBattle.Lib;
using System.Collections.Concurrent;


public class ISenderAdapter : ISender
{
    private BlockingCollection<ICommand> queue;

    public ISenderAdapter(BlockingCollection<ICommand> queue)
    {
        this.queue = queue;
    }

    public void Send(ICommand message)
    {
        this.queue.Add(message);
    }
}
