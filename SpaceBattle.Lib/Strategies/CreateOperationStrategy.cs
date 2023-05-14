using Hwdtech;

namespace SpaceBattle.Lib;


public class CreateOperationStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        var type = (string)args[1];

        var ruleList = IoC.Resolve<IEnumerable<string>>("Game.Rules.Get." + type);

        var commandList = ruleList.ToList().Select(rule => IoC.Resolve<ICommand>("Game.Command." + rule, obj));

        return IoC.Resolve<ICommand>("Game.Command.Macro.Create", commandList);
    }
}
