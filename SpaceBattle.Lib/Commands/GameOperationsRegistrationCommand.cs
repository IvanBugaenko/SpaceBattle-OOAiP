using Hwdtech;

namespace SpaceBattle.Lib;

public class GameOperationsRegistrationCommand : ICommand
{
    private int gameID;
    public GameOperationsRegistrationCommand(int gameID)
    {
        this.gameID = gameID;
    }

    public void Execute()
    {
        var cmd = IoC.Resolve<ICommand>("Thread.Games.Operations.Registration");
        IoC.Resolve<ICommand>("Game.Queue.Push.ByID", gameID, cmd).Execute();
    }
}
