namespace SpaceBattle.Lib;

public interface IStartable
{
    public IUObject Target
    {
        get;
    }
    
    public IDictionary<string, object> Properties
    {
        get;
    }
}
