namespace SpaceBattle.Lib;

public class CreateRotateCommandStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        return new RotateCommand((IRotatable)args[0]);
    }
}
