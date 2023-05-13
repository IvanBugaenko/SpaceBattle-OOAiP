using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;


public class CreateRuleInitializationCommandStrategyTest
{
    [Fact]
    public void SuccessfulCreateRuleInitializationCommandStrategyRunStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var rulesList = new List<string>(){
            "Rotate"
        };

        var getGameRulesStrategy = new Mock<IStrategy>();
        getGameRulesStrategy.Setup(s => s.RunStrategy()).Returns(rulesList).Verifiable();

        var strategyMap = new Dictionary<string, IStrategy>()
        {
            {"Rotate", new CreateRotateCommandStrategy()},
            {"Shoot", new CreateShootCommandStrategy()}
        };

        var getStrategyMapStrategy = new Mock<IStrategy>();
        getStrategyMapStrategy.Setup(s => s.RunStrategy()).Returns(strategyMap).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Rules.Get", (object[] args) => getGameRulesStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Base.Strategies.Get", (object[] args) => getStrategyMapStrategy.Object.RunStrategy(args)).Execute();

        var strategy = new CreateRuleInitializationCommandStrategy();
        var actionCommand = (ICommand)strategy.RunStrategy();

        Assert.IsType<ActionCommand>(actionCommand);

        actionCommand.Execute();

        getStrategyMapStrategy.Verify();
        getGameRulesStrategy.Verify();

        var rotatable = new Mock<IRotatable>();

        Assert.IsType<RotateCommand>(IoC.Resolve<ICommand>("Game.CreateCommand.Rotate", rotatable.Object));
    }
}
