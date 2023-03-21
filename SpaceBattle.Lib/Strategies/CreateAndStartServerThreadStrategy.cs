using Hwdtech;
using System.Collections.Concurrent;

namespace SpaceBattle.Lib;

public class CreateAndStartServerThreadStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {}; 

        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        var cmd = new CreateAndStartServerThreadCommand(id);

        return new ActionCommand(() => {
            cmd.Execute();
            action();
        });
    }
}
