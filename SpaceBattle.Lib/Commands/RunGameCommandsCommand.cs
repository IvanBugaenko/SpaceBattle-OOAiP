using Hwdtech;
using System.Diagnostics;

namespace SpaceBattle.Lib;


public class RunGameCommandsCommand : ICommand
{
    private Queue<ICommand> queue;
    private double quentum;

    public RunGameCommandsCommand(Queue<ICommand> queue, double quentum)
    {
        this.queue = queue;
        this.quentum = quentum;
    }

    public void Execute()
    {
        var stopWatch = new Stopwatch();

        stopWatch.Start();

        while (stopWatch.Elapsed.TotalMilliseconds <= this.quentum)
        {
            var cmd = IoC.Resolve<ICommand>("Game.Queue.GetCommand", queue);

            try 
            {
                cmd.Execute();
            } 
            catch (Exception e)
            {
                IoC.Resolve<IHandler>("GetExceptionHandler", new List<Type>{cmd.GetType(), e.GetType()}).Handle();
            }
        }

        stopWatch.Stop();
    }
}
