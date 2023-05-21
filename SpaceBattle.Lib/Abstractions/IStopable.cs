namespace SpaceBattle.Lib;

public interface IStopable
{
    public IEnumerable<string> Properties
    {
        get;
    }
    
    public IUObject Target
    {
        get;
    }
}
