using Moq;

namespace SpaceBattle.Lib.Test;

public class RotateTests
{
    [Fact]
    public void RotateGood()
    {
        var m = new Mock<IRotatable>();
        m.Setup(a => a.Angle).Returns(45).Verifiable();
        m.Setup(a => a.AngleSpeed).Returns(90).Verifiable();
        var c = new RotateCommand(m.Object);
        
        c.Execute();

        m.VerifySet(a => a.Angle = 135, Times.Once);
    }

    [Fact]
    public void SetAngleErr()
    {
        Mock<IRotatable> m = new Mock<IRotatable>();
        m.SetupProperty(m => m.Angle, 45);
        m.SetupGet<int>(m => m.AngleSpeed).Returns(90);
        m.SetupSet(m => m.Angle = It.IsAny<int>()).Throws<Exception>();
        var c = new RotateCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }

    [Fact]
    public void GetAngleSpeedErr()
    {
        Mock<IRotatable> m = new Mock<IRotatable>();
        m.SetupProperty(m => m.Angle, 45);
        m.SetupGet<int>(m => m.AngleSpeed).Throws<Exception>();
        var c = new RotateCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }

    [Fact]
    public void GetAngleErr()
    {
        Mock<IRotatable> m = new Mock<IRotatable>();
        m.SetupProperty(m => m.Angle, 45);
        m.SetupGet<int>(m => m.AngleSpeed).Returns(90);
        m.SetupGet<int>(m => m.Angle).Throws<Exception>();
        var c = new RotateCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }
}