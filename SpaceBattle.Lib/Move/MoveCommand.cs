namespace SpaceBattle.Lib;

public class MoveCommand: ICommand
{
    private readonly IMovable moving;
    public MoveCommand(IMovable obj)
    {
        moving = obj;
    }
     public void Execute()
     {
        // moving.SetCoords(moving.GetCoords() + moving.GetSpeed());
        moving.Pos += moving.Speed;
     }
}