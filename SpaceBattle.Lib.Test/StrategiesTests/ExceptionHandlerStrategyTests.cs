using Hwdtech;
using Hwdtech.Ioc;

using Moq;

namespace SpaceBattle.Lib.Test;

public class ExceptionHandlerStrategyTests
{
    Mock<IHandler> mockHandler = new Mock<IHandler>();
    Mock<IStrategy> mockStrategyReturnsTree = new Mock<IStrategy>();

    public ExceptionHandlerStrategyTests()
    {
        new InitScopeBasedIoCImplementationCommand().Execute();
        IoC.Resolve<Hwdtech.ICommand>("Scopes.Current.Set", IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))).Execute();

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

        mockStrategyReturnsTree.Setup(m => m.RunStrategy()).Returns(tree_handler).Verifiable();

        IoC.Resolve<Hwdtech.ICommand>("IoC.Register", "Game.GetExceptionTree", (object[] args) => mockStrategyReturnsTree.Object.RunStrategy(args)).Execute();
    }

    [Fact]
    public void SuccesfulGetHandleForCommandAndException()
    {
        var strategy = new FindExceptionHandlerStrategy();

        var handler = (IHandler) strategy.RunStrategy(typeof(MoveCommand), typeof(ArgumentException));
        handler.Handle();

        mockHandler.VerifyAll();
        mockStrategyReturnsTree.VerifyAll();
    }

    [Fact]
    public void SuccesfulGetHandleForCommandAndWrongException()
    {
        var strategy = new FindExceptionHandlerStrategy();

        var handler = (IHandler) strategy.RunStrategy(typeof(MoveCommand), typeof(FormatException));
        handler.Handle();
        
        mockHandler.VerifyAll();
        mockStrategyReturnsTree.VerifyAll();
    }

    [Fact]
    public void SuccesfulGetHandleForWrongCommandAndWrongException()
    {
        var strategy = new FindExceptionHandlerStrategy();

        var handler = (IHandler) strategy.RunStrategy(typeof(RotateCommand), typeof(FormatException));
        handler.Handle();
        
        mockHandler.VerifyAll();
        mockStrategyReturnsTree.VerifyAll();
    }

    [Fact]
    public void SuccesfulGetHandleForWrongCommandAndException()
    {
        var strategy = new FindExceptionHandlerStrategy();

        var handler = (IHandler) strategy.RunStrategy(typeof(RotateCommand), typeof(ArgumentException));
        handler.Handle();
        
        mockHandler.VerifyAll();
        mockStrategyReturnsTree.VerifyAll();
    }
}
