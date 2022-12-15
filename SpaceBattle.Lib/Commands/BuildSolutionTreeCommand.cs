using Hwdtech;

namespace SpaceBattle.Lib;

public class BuildSolutionTreeCommand : ICommand
{
    private string path;
    public BuildSolutionTreeCommand(string path)
    {
        this.path = path;
    }
    public void Execute()
    { 
        var parametrs = File.ReadAllLines(path).ToList().Select(line => line.Split(" ").Select(int.Parse).ToList()).ToList();

        var tree = IoC.Resolve<IDictionary<int, object>>("Game.GetSolutionTree");

        parametrs.ForEach(list =>
        {
            var temp = tree;
            list.ForEach(num => 
            {
                temp.TryAdd(num, new Dictionary<int, object>()); 
                temp = (Dictionary<int, object>) temp[num];
            });
        });
    }
}
