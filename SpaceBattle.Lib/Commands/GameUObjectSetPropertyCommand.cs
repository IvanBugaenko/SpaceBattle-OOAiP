namespace SpaceBattle.Lib;

public class GameUObjectSetPropertyCommand: ICommand
{
    private IUObject obj;
    private string key;
    private object value;

    public GameUObjectSetPropertyCommand(IUObject obj, string key, object value)
    {
        this.obj = obj;
        this.key = key;
        this.value = value;
    }

    public void Execute()
    {
        obj.setProperty(this.key, this.value);
    }
}
