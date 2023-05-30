using Hwdtech;
using System.Reflection;

namespace SpaceBattle.Lib;


public class BuildAdapterCodeStringStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var adaptableType = (Type)args[0];
        var adaptiveType = (Type)args[1];

        var builder = IoC.Resolve<IBuilder>("Options.Builder.Adapter", adaptableType, adaptiveType);

        adaptiveType.GetProperties().ToList().ForEach(property => builder.AddProperty(property));

        return builder.Build();
    }
}
