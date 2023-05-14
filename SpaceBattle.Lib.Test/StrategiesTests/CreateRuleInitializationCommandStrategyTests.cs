using Moq;
using Hwdtech;
using Hwdtech.Ioc;

namespace SpaceBattle.Lib.Test;


public class RegisteringCreatingOperationsStrategyTest
{
    [Fact]
    public void SuccessfulRegisteringCreatingOperationStrategyRunStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var ruleList = new List<string>(){
            "Check"
        };

        var ruleListStrategy = new Mock<IStrategy>();
        ruleListStrategy.Setup(s => s.RunStrategy()).Returns(ruleList);

        var checkCommand = new Mock<ICommand>();
        checkCommand.Setup(c => c.Execute()).Callback(() => {}).Verifiable();

        var checkCommandStrategy = new Mock<IStrategy>();
        checkCommandStrategy.Setup(s => s.RunStrategy(It.IsAny<IUObject>())).Returns(checkCommand.Object);
        
        
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Rules.Get.Rotate", (object[] args) => ruleListStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Check", (object[] args) => checkCommandStrategy.Object.RunStrategy(args)).Execute();
        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Command.Macro.Create", (object[] args) => new MacroCommand(new List<ICommand>(){checkCommand.Object})).Execute();

        var strategy = new RegisteringCreatingOperationStrategy();
        var actionCommand = (ICommand)strategy.RunStrategy();

        Assert.IsType<ActionCommand>(actionCommand);

        actionCommand.Execute();

        var obj = new Mock<IUObject>();

        var result = IoC.Resolve<ICommand>("Game.Operations.Create", obj.Object, "Rotate");

        Assert.IsType<MacroCommand>(result);

        result.Execute();

        checkCommand.VerifyAll();
    }
}
