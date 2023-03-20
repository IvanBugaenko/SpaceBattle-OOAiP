using Hwdtech;
using System.Collections.Concurrent;
namespace SpaceBattle.Lib;

public class SendCommand: ICommand
{
    private int id;
    private ICommand message;

    public SendCommand(int id, ICommand message)
    {
        this.id = id;
        this.message = message;
    }

    public void Execute()
    {
        ISender ?queue;

        if (IoC.Resolve<ConcurrentDictionary<int, ISender>>("GetServrerThreadsSenders").TryGetValue(this.id, out queue))
        {
            queue.Send(this.message);
        }
        else throw new Exception();
    }
}
