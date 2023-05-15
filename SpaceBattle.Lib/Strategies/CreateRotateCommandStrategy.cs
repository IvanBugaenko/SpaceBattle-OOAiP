using Hwdtech;


namespace SpaceBattle.Lib;

public class CreateRotateCommandStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject)args[0];
        return new RotateCommand(IoC.Resolve<IRotatable>("Game.Adapter.Create", obj, typeof(IRotatable)));
    }
}
