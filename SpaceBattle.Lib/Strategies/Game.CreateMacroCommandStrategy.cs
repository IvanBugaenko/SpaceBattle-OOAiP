namespace SpaceBattle.Lib;

public class CreateMacroCommandStrategy : IStrategy
{

    public object RunStrategy(params object[] args)
    {
        return new MacroCommand((IEnumerable<ICommand>) args[0]);
    }
}
