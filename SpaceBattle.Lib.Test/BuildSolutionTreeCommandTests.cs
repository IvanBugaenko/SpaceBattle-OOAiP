using Hwdtech.Ioc;
using Hwdtech;

using Moq;

namespace SpaceBattle.Lib.Test;

public class SolutionTreeTests
{
    public SolutionTreeTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();

        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();
    
        var mockStrategyReturnsDict = new Mock<IStrategy>();
        mockStrategyReturnsDict.Setup(x => x.RunStrategy()).Returns(new Dictionary<int, object>());

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetSolutionTree", (object[] args) => mockStrategyReturnsDict.Object.RunStrategy(args)).Execute();
    }

    [Fact]
    public void SuccesfulBuildSolutionTree()
    {
        var path = @"..\..\..\file.txt";

        var buildCommand = new BuildSolutionTreeCommand(path);

        buildCommand.Execute();

        var t = IoC.Resolve<IDictionary<int, object>>("Game.GetSolutionTree");

        Assert.True(t.ContainsKey(1));
        Assert.True(t.ContainsKey(4));
        Assert.True(t.ContainsKey(9));
        Assert.True(t.ContainsKey(0));

        Assert.True(((IDictionary<int, object>) t[1]).ContainsKey(7));
        Assert.True(((IDictionary<int, object>) t[1]).ContainsKey(3));

        Assert.True(((IDictionary<int, object>)((IDictionary<int, object>) t[1])[7]).ContainsKey(9));
    }  
}
