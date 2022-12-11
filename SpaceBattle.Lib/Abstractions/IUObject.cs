namespace SpaceBattle.Lib;

public interface IUObject
{
    void setProperty(string key, object value);
    object getProperty(string key);
}
