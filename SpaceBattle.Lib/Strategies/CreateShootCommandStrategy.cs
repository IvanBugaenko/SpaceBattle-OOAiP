namespace SpaceBattle.Lib;

public class CreateShootCommandStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        return new ShootCommand((IShootable)args[0]);
    }
}
