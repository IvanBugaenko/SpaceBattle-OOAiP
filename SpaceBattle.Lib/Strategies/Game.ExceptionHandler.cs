using Hwdtech;

namespace SpaceBattle.Lib;

public class FindExceptionHandlerStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var cmd = (Type)args[0];
        var exception = (Type)args[1];

        var tree = IoC.Resolve<IDictionary<object, IDictionary<object, IHandler>>>("Game.GetExceptionTree");

        if (!tree.TryGetValue(cmd, out IDictionary<object, IHandler>? dict))
        {
            var tree_exception = tree["default"];
            if (!tree_exception.TryGetValue(exception, out IHandler? handler))
                return tree_exception["default"];
            return handler;
        }
        return (dict.TryGetValue(exception, out IHandler? handler1) ? handler1 : dict["default"]);
    }
}
