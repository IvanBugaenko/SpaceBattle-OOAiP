using Hwdtech;

namespace SpaceBattle.Lib;

public class PrepareDataToCollisionStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        List<int> list = new List<int>();

        var prop1 = (List<int>)args[0];
        var prop2 = (List<int>)args[1];

        prop1.ForEach(n => list.Add(n - prop2[prop1.IndexOf(n)]));

        return list;
    }
}
