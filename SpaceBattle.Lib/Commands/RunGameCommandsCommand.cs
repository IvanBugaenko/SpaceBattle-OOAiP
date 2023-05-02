using Hwdtech;
using System.Diagnostics;

namespace SpaceBattle.Lib;


public class RunGameCommandsCommand : ICommand
{
    private Queue<ICommand> queue;
    private double quantum;

    public RunGameCommandsCommand(Queue<ICommand> queue, double quantum)
    {
        this.queue = queue;
        this.quantum = quantum;
    }

    public void Execute()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();

        while (stopWatch.Elapsed.TotalMilliseconds <= this.quantum)
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
