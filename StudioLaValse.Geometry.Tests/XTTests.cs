namespace StudioLaValse.Geometry.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class XYTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var x = 3.5;
        var y = 4.5;

        // Act
        var point = new XY(x, y);

        // Assert
        Assert.AreEqual(x, point.X);
        Assert.AreEqual(y, point.Y);
    }

    [TestMethod]
    public void Average_ShouldCalculateCorrectly()
    {
        // Arrange
        var points = new List<XY>
        {
            new XY(1, 1),
            new XY(3, 3),
            new XY(5, 5)
        };

        // Act
        var average = XY.Average(points);

        // Assert
        Assert.AreEqual(3, average.X);
        Assert.AreEqual(3, average.Y);
    }

    [TestMethod]
    public void DistanceTo_ShouldCalculateCorrectly()
    {
        // Arrange
        var point1 = new XY(1, 1);
        var point2 = new XY(4, 5);

        // Act
        var distance = point1.DistanceTo(point2);

        // Assert
        Assert.AreEqual(5, distance);
    }

    [TestMethod]
    public void Length_ShouldCalculateCorrectly()
    {
        // Arrange
        var point = new XY(3, 4);

        // Act
        var length = point.Length();

        // Assert
        Assert.AreEqual(5, length);
    }

    [TestMethod]
    public void Normalize_ShouldNormalizeCorrectly()
    {
        // Arrange
        var point = new XY(3, 4);

        // Act
        var normalized = point.Normalize();

        // Assert
        Assert.IsTrue(normalized.X.AlmostEqualTo(0.6));
        Assert.IsTrue(normalized.Y.AlmostEqualTo(0.8));
    }

    [TestMethod]
    public void Move_ShouldMoveCorrectly()
    {
        // Arrange
        var point = new XY(1, 1);
        var length = Math.Sqrt(2);
        var angle = Math.PI / 4; // 45 degrees

        // Act
        var moved = point.Move(length, angle);

        // Assert
        Assert.IsTrue(moved.X.AlmostEqualTo(2));
        Assert.IsTrue(moved.Y.AlmostEqualTo(2));
    }

    [TestMethod]
    public void Operator_Addition_ShouldAddCorrectly()
    {
        // Arrange
        var point1 = new XY(1, 1);
        var point2 = new XY(2, 2);

        // Act
        var result = point1 + point2;

        // Assert
        Assert.AreEqual(3, result.X);
        Assert.AreEqual(3, result.Y);
    }

    [TestMethod]
    public void Operator_Subtraction_ShouldSubtractCorrectly()
    {
        // Arrange
        var point1 = new XY(5, 5);
        var point2 = new XY(3, 2);

        // Act
        var result = point1 - point2;

        // Assert
        Assert.AreEqual(2, result.X);
        Assert.AreEqual(3, result.Y);
    }

    [TestMethod]
    public void Operator_Division_ShouldDivideCorrectly()
    {
        // Arrange
        var point = new XY(6, 8);
        double divisor = 2;

        // Act
        var result = point / divisor;

        // Assert
        Assert.AreEqual(3, result.X);
        Assert.AreEqual(4, result.Y);
    }

    [TestMethod]
    public void Operator_Multiplication_ShouldMultiplyCorrectly()
    {
        // Arrange
        var point = new XY(1.5, 2);
        double multiplier = 2;

        // Act
        var result = point * multiplier;

        // Assert
        Assert.AreEqual(3, result.X);
        Assert.AreEqual(4, result.Y);
    }
}
