using Hwdtech;

namespace SpaceBattle.Lib;


public class ShootCommand: ICommand
{
    private IShootable obj;
    public ShootCommand(IShootable obj)
    {
        this.obj = obj;
    }

    public void Execute()
    {
        IoC.Resolve<ICommand>("Game.Queue.Push", IoC.Resolve<Queue<ICommand>>("Game.Queue.Get"),
            IoC.Resolve<ICommand>("Game.IUObject.Shoot", obj)).Execute();
    }
}
