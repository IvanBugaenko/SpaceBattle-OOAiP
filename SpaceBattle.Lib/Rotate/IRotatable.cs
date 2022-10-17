namespace SpaceBattle.Lib;

public interface IRotatable
{
    MyAngle Angle
    {
        get;
        set;
    }
    MyAngle AngleSpeed
    {
        get;
    }
}