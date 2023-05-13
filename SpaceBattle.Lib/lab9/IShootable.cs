namespace SpaceBattle.Lib;


public interface IShootable
{
    IDictionary<object, object> Bullets
    {
        get;
        set;
    }
}
