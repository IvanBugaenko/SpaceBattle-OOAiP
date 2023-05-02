using Hwdtech;
using System.Diagnostics;

namespace SpaceBattle.Lib;


public class RunGameCommandsCommand : ICommand
{
    private Queue<ICommand> queue;

    public RunGameCommandsCommand(Queue<ICommand> queue)
    {
        this.queue = queue;
    }

    public void Execute()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();

        while (stopWatch.Elapsed.TotalMilliseconds < IoC.Resolve<double>("Game.GetQuantumOfTime"))
        {
            var cmd = IoC.Resolve<ICommand>("Game.Queue.GetCommand", queue);

            try 
            {
                cmd.Execute();
            } 
            catch (Exception e)
            {
                IoC.Resolve<IHandler>("Game.GetExceptionHandler", cmd, e).Handle();
            }
        }

        stopWatch.Stop();
    }
}
