namespace SpaceBattle.Lib;

public class SendCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        var message = (ICommand)args[1];

        return new SendCommand(id, message);
    }
}
