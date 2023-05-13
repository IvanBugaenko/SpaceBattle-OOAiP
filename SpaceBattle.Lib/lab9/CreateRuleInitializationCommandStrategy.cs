using Hwdtech;

namespace SpaceBattle.Lib;


public class CreateRuleInitializationCommandStrategy : IStrategy
{
    public object RunStrategy(params object[] args)
    {
        var rules = IoC.Resolve<IEnumerable<string>>("Game.Rules.Get");

        var strategyMap = IoC.Resolve<IDictionary<string, IStrategy>>("Base.Strategies.Get");

        return new ActionCommand(() => {
            rules.ToList().ForEach(rule => 
                IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.CreateCommand." + rule, 
                    (object[] args) => strategyMap[rule].RunStrategy(args)));
        });
    }
}
