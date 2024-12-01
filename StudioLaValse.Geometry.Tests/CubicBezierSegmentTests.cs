using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudioLaValse.Geometry;
using System.Collections.Generic;

namespace StudioLaValse.Geometry.Tests;

[TestClass]
public class CubicBezierSegmentTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var p0 = new XY(0, 0);
        var p1 = new XY(1, 2);
        var p2 = new XY(3, 3);
        var p3 = new XY(4, 0);

        // Act
        var bezierSegment = new CubicBezierSegment(p0, p1, p2, p3);

        // Assert
        Assert.AreEqual(p0, bezierSegment.FirstControlPoint);
        Assert.AreEqual(p1, bezierSegment.SecondControlPoint);
        Assert.AreEqual(p2, bezierSegment.ThirdControlPoint);
        Assert.AreEqual(p3, bezierSegment.FourthControlPoint);
    }

    [TestMethod]
    public void GetBezierPoint_ShouldCalculateCorrectly()
    {
        // Arrange
        var p0 = new XY(-16, -4);
        var p1 = new XY(-10, 10);
        var p2 = new XY(8, -4);
        var p3 = new XY(16, 10);
        var bezierSegment = new CubicBezierSegment(p0, p1, p2, p3);

        // Act
        var point = bezierSegment.GetBezierPoint(0.5);

        // Assert
        Assert.IsTrue(point.X.AlmostEqualTo(-0.75));
        Assert.IsTrue(point.Y.AlmostEqualTo(3));
    }

    [TestMethod]
    public void ToPolyline_ShouldApproximateCorrectly()
    {
        // Arrange
        var p0 = new XY(0, 0);
        var p1 = new XY(1, 2);
        var p2 = new XY(3, 3);
        var p3 = new XY(4, 0);
        var bezierSegment = new CubicBezierSegment(p0, p1, p2, p3);
        var segments = 10;

        // Act
        var polyline = bezierSegment.ToPolyline(segments);

        // Assert
        Assert.AreEqual(segments + 1, polyline.Points.Count);
    }

    [TestMethod]
    public void ClosestPoint_ShouldFindCorrectPoint()
    {
        // Arrange
        var p0 = new XY(0, 0);
        var p1 = new XY(1, 2);
        var p2 = new XY(3, 3);
        var p3 = new XY(4, 0);
        var bezierSegment = new CubicBezierSegment(p0, p1, p2, p3);
        var targetPoint = new XY(2, 2);
        var expectedPoint = new XY(2, 1.88);

        // Act
        var closestPoint = bezierSegment.ClosestPoint(targetPoint);

        // Assert
        // The exact closest point may require manual validation or a more detailed implementation.
        // For this example, we can assert that the point is roughly close to the target.
        Assert.IsTrue(closestPoint.DistanceTo(expectedPoint) < 0.1);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void ClosestPoint_NoSegments_ShouldThrowException()
    {
        // Arrange
        var p0 = new XY(0, 0);
        var p1 = new XY(1, 2);
        var p2 = new XY(3, 3);
        var p3 = new XY(4, 0);
        var bezierSegment = new CubicBezierSegment(p0, p1, p2, p3);
        var targetPoint = new XY(2, 1);

        // Act
        var closestPoint = bezierSegment.ClosestPoint(targetPoint, 0);
    }
}
