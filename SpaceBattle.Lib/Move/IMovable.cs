namespace SpaceBattle.Lib;

public interface IMovable
{
    Vector Speed
    {
        get;
    }
    Vector Pos
    {
        get;
        set;
    }
}