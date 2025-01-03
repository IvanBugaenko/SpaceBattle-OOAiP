using Hwdtech;

namespace SpaceBattle.Lib;

public class DetectCollisionCommand : ICommand
{
    private IUObject obj1, obj2;
    public DetectCollisionCommand(IUObject obj1, IUObject obj2)
    {
        this.obj1 = obj1;
        this.obj2 = obj2;
    }

    public void Execute()
    {
        var tree = IoC.Resolve<IDictionary<int, object>>("Game.GetSolutionTree");

        var property1 = IoC.Resolve<List<int>>("Game.IUObject.UnionPropertiesForCollision", obj1);
        var property2 = IoC.Resolve<List<int>>("Game.IUObject.UnionPropertiesForCollision", obj2);

        var parametrs = IoC.Resolve<List<int>>("Game.PrepareDataToCollision", property1, property2);

        foreach (var i in parametrs)
        {
            if (!tree.TryGetValue(i, out object? value)) return;
            tree = (IDictionary<int, object>)value;
        }

        IoC.Resolve<ICommand>("Game.Collision", obj1, obj2).Execute();
    }
}
