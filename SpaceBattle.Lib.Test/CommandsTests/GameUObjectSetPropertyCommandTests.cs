using Moq;

namespace SpaceBattle.Lib.Test;

public class GameUObjectSetPropertyCommandTests
{
    [Fact]
    public void SuccessfulGameUObjectSetPropertyCommandExecute()
    {
        var obj = new Mock<IUObject>();
        obj.Setup(o => o.setProperty(It.IsAny<string>(), It.IsAny<object>())).Callback(() => {}).Verifiable();

        var gameUObjectSetPropertyCommand = new GameUObjectSetPropertyCommand(obj.Object, "lkf", 5);

        gameUObjectSetPropertyCommand.Execute();

        obj.VerifyAll();
    }
}
