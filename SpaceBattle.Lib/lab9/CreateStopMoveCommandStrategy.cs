namespace SpaceBattle.Lib;

public class CreateStopMoveCommandStrategy: IStrategy
{
    public object RunStrategy(params object[] args)
    {
        return new StopCommand((IStopable)args[0]);
    }
}
