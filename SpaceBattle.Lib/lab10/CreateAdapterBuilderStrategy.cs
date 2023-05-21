namespace SpaceBattle.Lib;


public class CreateAdapterBuilderStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var adaptableType = (Type)args[0];
        var adaptiveType = (Type)args[1];

        return new AdapterBuilder(adaptableType, adaptiveType);
    }
}
