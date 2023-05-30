using Hwdtech;

namespace SpaceBattle.Lib;


public class CreateOperationStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        var orderType = (string)args[1];

        var ruleList = IoC.Resolve<IEnumerable<string>>("Game.Rules.Get." + orderType);

        var commandList = ruleList.ToList().Select(rule => IoC.Resolve<ICommand>("Game.Command.Create." + rule, obj));

        return IoC.Resolve<ICommand>("Game.Command.Macro.Create", commandList);
    }
}
