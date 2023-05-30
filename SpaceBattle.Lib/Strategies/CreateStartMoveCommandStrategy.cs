using Hwdtech;


namespace SpaceBattle.Lib;

public class CreateStartMoveCommandStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new StartCommand(IoC.Resolve<IStartable>("Game.Adapter.Create", obj, typeof(IStartable)));
    }
}
