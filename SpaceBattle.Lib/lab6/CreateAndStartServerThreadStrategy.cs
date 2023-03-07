namespace SpaceBattle.Lib;

public class CreateAndStartServerThreadStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var id = (int)args[0];
        
        if (args[1] is not null)
        {
            
        }
    }
}