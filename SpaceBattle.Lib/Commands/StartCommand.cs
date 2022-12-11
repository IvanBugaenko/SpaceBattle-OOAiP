using Hwdtech;

namespace SpaceBattle.Lib;

public class StartMoveCommand : ICommand
{
    private IStartable obj;
    public StartMoveCommand(IStartable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        obj.Properties.ToList().ForEach(o => IoC.Resolve<ICommand>("Game.Commands.SetProperty", obj.Target, o.Key, o.Value).Execute());

        ICommand cmd = IoC.Resolve<ICommand>("Game.Operations.Moving", obj.Target);

        IoC.Resolve<ICommand>("Game.Commands.SetProperty", obj.Target, "Moving", cmd).Execute();

        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<Queue<ICommand>>("Game.Queue"), cmd).Execute();
    }
}
