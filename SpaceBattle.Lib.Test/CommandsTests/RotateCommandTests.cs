using Moq;

namespace SpaceBattle.Lib.Test;

public class RotateTests
{
    [Fact]
    public void Successful_Rotate()
    {
        var m = new Mock<IRotatable>();
        m.Setup(a => a.Angle).Returns(new MyAngle(45, 1)).Verifiable();
        m.Setup(a => a.AngleSpeed).Returns(new MyAngle(90, 1)).Verifiable();
        var c = new RotateCommand(m.Object);
        
        c.Execute();

        m.VerifySet(a => a.Angle = new MyAngle(135, 1), Times.Once);
    }

    [Fact]
    public void SetAngleFailed()
    {
        Mock<IRotatable> m = new Mock<IRotatable>();
        m.SetupProperty(m => m.Angle, new MyAngle(45, 1));
        m.SetupGet<MyAngle>(m => m.AngleSpeed).Returns(new MyAngle(90, 1));
        m.SetupSet(m => m.Angle = It.IsAny<MyAngle>()).Throws<Exception>();
        var c = new RotateCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }

    [Fact]
    public void GetAngleSpeedFailed()
    {
        Mock<IRotatable> m = new Mock<IRotatable>();
        m.SetupProperty(m => m.Angle, new MyAngle(45, 1));
        m.SetupGet<MyAngle>(m => m.AngleSpeed).Throws<Exception>();
        var c = new RotateCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }

    [Fact]
    public void GetAngleFailed()
    {
        Mock<IRotatable> m = new Mock<IRotatable>();
        m.SetupProperty(m => m.Angle, new MyAngle(45, 1));
        m.SetupGet<MyAngle>(m => m.AngleSpeed).Returns(new MyAngle(90, 1));
        m.SetupGet<MyAngle>(m => m.Angle).Throws<Exception>();
        var c = new RotateCommand(m.Object);

        Assert.Throws<Exception>(() => c.Execute());
    }
}
