namespace SpaceBattle.Lib;

public interface IMovable
{
    public Vector Speed
    {
        get;
    }
    
    public Vector Pos
    {
        get;
        set;
    }
}
