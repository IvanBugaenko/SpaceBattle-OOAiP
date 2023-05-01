using Hwdtech;

namespace SpaceBattle.Lib;


public class GameCommand : ICommand
{
    private Queue<ICommand> queue;
    private object scope;

    public GameCommand(Queue<ICommand> queue, object scope)
    {
        this.queue = queue;
        this.scope = scope;
    }

    public void Execute()
    {
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", this.scope).Execute();

        var q = IoC.Resolve<double>("Game.GetQuantumOfTime");
        
        IoC.Resolve<ICommand>("Game.RunCommands", this.queue, q).Execute();
    }
}
