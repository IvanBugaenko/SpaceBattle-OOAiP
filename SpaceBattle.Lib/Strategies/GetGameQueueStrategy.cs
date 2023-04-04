using Hwdtech;
namespace SpaceBattle.Lib;

public class GetGameQueueStartegy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];

        if (!IoC.Resolve<IDictionary<int, Queue<ICommand>>>("GameQueues").TryGetValue(id, out Queue<ICommand>? queue))
            throw new Exception();

        return queue;
    }
}
