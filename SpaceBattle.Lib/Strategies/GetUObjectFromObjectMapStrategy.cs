using Hwdtech;
namespace SpaceBattle.Lib;

public class GetUObjectFromObjectMapStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];

        if (!IoC.Resolve<IDictionary<int, IUObject>>("GetUObjecs").TryGetValue(id, out IUObject? obj))
            throw new Exception();

        return obj; 
    }
}
