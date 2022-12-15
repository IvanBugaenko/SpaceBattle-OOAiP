namespace SpaceBattle.Lib;

public class GetPropertyStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var obj = (IUObject) args[0];
        var key = (string) args[1];
        return obj.getProperty(key);
    }
}
