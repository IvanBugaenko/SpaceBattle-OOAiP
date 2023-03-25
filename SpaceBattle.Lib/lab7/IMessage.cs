namespace SpaceBattle.Lib;

public interface IMessage
{
    public string OrderType
    {
        get;
    }

    public int GameID
    {
        get;
    }

    public int Target
    {
        get;
    }

    public IDictionary<string, object> Properties
    {
        get;
    }
}
