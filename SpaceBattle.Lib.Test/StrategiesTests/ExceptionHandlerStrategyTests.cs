using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class ExceptionHandlerStrategyTests
{
    [Fact]
    public void SuccesfulGetPropertyStrategy()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

        var mockHandler = new Mock<IHandler>();
        mockHandler.Setup(m => m.Handle()).Verifiable();

        var tree_handler = new Dictionary<object, IDictionary<object, IHandler>>(){
            {typeof(MoveCommand), new Dictionary<object, IHandler>(){
                    {typeof(ArgumentException), mockHandler.Object},
                    {"default", mockHandler.Object}
                }
            },
            {"default", new Dictionary<object, IHandler>(){
                    {typeof(ArgumentException), mockHandler.Object},
                    {"default", mockHandler.Object}
                }
            }
        };

        var mockStrategyReturnsTree = new Mock<IStrategy>();
        mockStrategyReturnsTree.Setup(m => m.RunStrategy()).Returns(tree_handler).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetExceptionTree", (object[] args) => mockStrategyReturnsTree.Object.RunStrategy(args)).Execute();
        var strategy = new FindExceptionHandlerStrategy();

        var handler1 = (IHandler) strategy.RunStrategy(typeof(MoveCommand), typeof(ArgumentException));
        handler1.Handle();

        var handler2 = (IHandler) strategy.RunStrategy(typeof(MoveCommand), typeof(FormatException));
        handler2.Handle();

        var handler3 = (IHandler) strategy.RunStrategy(typeof(RotateCommand), typeof(FormatException));
        handler3.Handle();

        var handler4 = (IHandler) strategy.RunStrategy(typeof(RotateCommand), typeof(ArgumentException));
        handler4.Handle();
        
        mockHandler.VerifyAll();
        mockStrategyReturnsTree.VerifyAll();
    }
}