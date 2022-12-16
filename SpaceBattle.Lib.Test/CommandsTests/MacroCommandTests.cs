using Moq;

namespace SpaceBattle.Lib.Test;

public class MacroCommandTests
{
    [Fact]
    public void SuccesfulMacroCommandTestsExecute()
    {
        var mockCommand = new Mock<ICommand>();
        mockCommand.Setup(m => m.Execute()).Verifiable();

        var list = new List<ICommand>(){mockCommand.Object};

        var cmd = new MacroCommand(list);

        cmd.Execute();
        
        mockCommand.VerifyAll();
    }
}
