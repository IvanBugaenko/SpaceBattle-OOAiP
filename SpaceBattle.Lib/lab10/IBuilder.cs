using System.Reflection;

namespace SpaceBattle.Lib;


public interface IBuilder
{
    public String Build();
    public void AddProperty(object property);
}
