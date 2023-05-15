using Hwdtech;


namespace SpaceBattle.Lib;

public class CreateStopMoveCommandStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new StopCommand(IoC.Resolve<IStopable>("Game.Adapter.Create", obj, typeof(IStopable)));
    }
}
