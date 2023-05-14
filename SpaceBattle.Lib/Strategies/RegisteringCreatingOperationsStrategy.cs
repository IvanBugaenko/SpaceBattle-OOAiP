using Hwdtech;

namespace SpaceBattle.Lib;


public class RegisteringCreatingOperationStrategy : IStrategy
{
    IStrategy createOperationStrategy = new CreateOperationStrategy();

    public object RunStrategy(params object[] args)
    {
        return new ActionCommand(() => {
            IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.Operations.Create", (object[] args) => this.createOperationStrategy.RunStrategy(args)).Execute();
        });
    }
}
