using Moq;

namespace SpaceBattle.Lib.Test;

public class MoveTests
{
    [Fact]
    public void MoveGood()
    {
        var m = new Mock<IMovable>();
        m.Setup(a => a.Pos).Returns(new Vector(12, 5)).Verifiable();
        m.Setup(a => a.Speed).Returns(new Vector(-7, 3)).Verifiable();
        var c = new MoveCommand(m.Object);
        
        c.Execute();

        m.VerifySet(a => a.Pos = new Vector(5, 8), Times.Once);
    }

    [Fact]
    public void SetPosErr()
    {
        Mock<IMovable> m = new Mock<IMovable>();
        m.SetupProperty(m => m.Pos, new Vector(12, 5));
        m.SetupGet<Vector>(m => m.Speed).Returns(new Vector(-7, 3));
        m.SetupSet(m => m.Pos = It.IsAny<Vector>()).Throws<Exception>();
        var c = new MoveCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }

    [Fact]
    public void GetSpeedErr()
    {
        Mock<IMovable> m = new Mock<IMovable>();
        m.SetupProperty(m => m.Pos, new Vector(12, 5));
        m.SetupGet<Vector>(m => m.Speed).Throws<Exception>();
        var c = new MoveCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }

    [Fact]
    public void GetPosErr()
    {
        Mock<IMovable> m = new Mock<IMovable>();
        m.SetupProperty(m => m.Pos, new Vector(12, 5));
        m.SetupGet<Vector>(m => m.Speed).Returns(new Vector(-7, 3));
        m.SetupGet<Vector>(m => m.Pos).Throws<Exception>();
        var c = new MoveCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }
}