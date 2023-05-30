namespace SpaceBattle.Lib;


public interface IShootable
{
    public String ProjectileType
    {
        get;
        set;
    }

    public Vector Pos
    {
        get;
        set;
    }
    public Vector Speed
    {
        get;
    }
}
