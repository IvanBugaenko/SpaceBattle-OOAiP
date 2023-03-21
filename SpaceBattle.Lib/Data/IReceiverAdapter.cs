namespace SpaceBattle.Lib;

using System.Collections.Concurrent;


public class IReceiverAdapter : IReceiver
{
    private BlockingCollection<ICommand> queue;

    public IReceiverAdapter(BlockingCollection<ICommand> queue)
    {
        this.queue = queue;
    }

    public bool isEmpty()
    {
        return this.queue.Count() == 0;
    }

    public ICommand Receive()
    {
        return this.queue.Take();
    }
}
