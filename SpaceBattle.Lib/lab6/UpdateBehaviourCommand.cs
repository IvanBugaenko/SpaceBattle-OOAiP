namespace SpaceBattle.Lib;

public class UpdateBehaviourCommand: ICommand
{
    private ServerThread thread;
    private Action newBehaviour;

    public UpdateBehaviourCommand(ServerThread thread, Action newBehaviour)
    {
        this.thread = thread;

        this.newBehaviour = newBehaviour;
    }

    public void Execute()
    {
        this.thread.UpdateBehaviour(this.newBehaviour);
    }
}
