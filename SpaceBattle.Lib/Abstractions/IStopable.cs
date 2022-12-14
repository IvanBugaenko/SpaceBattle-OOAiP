namespace SpaceBattle.Lib;

public interface IStopable
{
    IEnumerable<string> Properties
    {
        get;
    }
    IUObject Target
    {
        get;
    }
}
