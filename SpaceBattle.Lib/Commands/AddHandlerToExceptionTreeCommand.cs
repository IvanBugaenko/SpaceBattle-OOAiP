using Hwdtech;

namespace SpaceBattle.Lib;

public class AddHandlerToExceptionTreeCommand: ICommand
{
    private IEnumerable<Type> list;
    private IHandler handler;
    public AddHandlerToExceptionTreeCommand(IEnumerable<Type> list, IHandler handler)
    {
        this.list = list;
        this.handler = handler;
    }
    public void Execute()
    {
        var tree = IoC.Resolve<IDictionary<int, IHandler>>("Game.GetExceptionTree");

        var key = IoC.Resolve<int>("Game.GetHashCode", list);

        tree.TryAdd(key, handler);
    }
}
