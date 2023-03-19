namespace SpaceBattle.Lib;
using Hwdtech;
using System.Collections.Concurrent;

public class SoftStopServerThreadStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var action = () => {}; 

        if (args.Length == 2)
        {
            action = (Action)args[1];
        }

        var cmd = new SoftStopServerThreadCommand(id, action);

        return new SendCommand(id, new ActionCommand(() => {
            cmd.Execute();
        }));
    }
}
