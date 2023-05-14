using Hwdtech;

namespace SpaceBattle.Lib;

public class GameRuleRegistrationCommand : ICommand
{
    private int gameID;
    public GameRuleRegistrationCommand(int gameID)
    {
        this.gameID = gameID;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>("Thread.Command.Create.Rule.Initialization");
        IoC.Resolve<ICommand>("Game.Queue.Push.ByID", gameID, cmd).Execute();
    }
}
