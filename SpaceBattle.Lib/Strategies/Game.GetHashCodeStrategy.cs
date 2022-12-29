namespace SpaceBattle.Lib;

public class GetHashCodeStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var list = (IEnumerable<object>)args[0];

        unchecked
        {
            int hashcode = (int)232582681769;
            list.Select(t => t.GetHashCode()).OrderBy(t => t).ToList().ForEach(n => hashcode = (hashcode * 7302013) ^ n);
            return hashcode;
        }
    }
}
