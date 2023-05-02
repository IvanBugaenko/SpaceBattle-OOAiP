using Hwdtech;
using Hwdtech.Ioc;
using Moq;

namespace SpaceBattle.Lib.Test;


public class DefaultGameHandlerTests
{
    [Fact]
    public void SuccessfulThrowExceptionWithInformationAboutCommand()
    {
        var cmd = new Mock<ICommand>();
        var exception = new Exception();

        var handler = new DefaultGameHandler(cmd.Object, exception);

        try
        {
            handler.Handle();
        }
        catch (Exception e)
        {
            Assert.Equal(cmd.Object, e.Data[cmd.Object.GetType()]);
        }
    }
}
