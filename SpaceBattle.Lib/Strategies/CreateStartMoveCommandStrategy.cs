namespace SpaceBattle.Lib;

public class CreateStartMoveCommandStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        return new StartCommand((IStartable)args[0]);
    }
}
