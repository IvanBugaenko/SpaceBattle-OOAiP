namespace SpaceBattle.Lib;

public interface IRotatable
{
    public MyAngle Angle
    {
        get;
        set;
    }
    
    public MyAngle AngleSpeed
    {
        get;
    }
}
