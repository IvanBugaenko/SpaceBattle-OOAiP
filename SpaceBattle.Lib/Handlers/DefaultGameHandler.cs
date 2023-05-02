namespace SpaceBattle.Lib;


public class DefaultGameHandler : IHandler
{
    private ICommand cmd;
    private Exception exception;
    public DefaultGameHandler(ICommand cmd, Exception exception)
    {
        this.cmd = cmd;
        this.exception = exception;
    }

    public void Handle()
    {
        this.exception.Data.Add(this.cmd.GetType(), this.cmd);
        throw this.exception;
    }
}
