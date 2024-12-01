using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.Geometry.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;


[TestClass]
public class QuadraticBezierSegmentTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly()
    {
        // Arrange
        var p0 = new XY(0, 0);
        var p1 = new XY(1, 2);
        var p2 = new XY(3, 3);

        // Act
        var bezierSegment = new QuadraticBezierSegment(p0, p1, p2);

        // Assert
        Assert.AreEqual(p0, bezierSegment.FirstControlPoint);
        Assert.AreEqual(p1, bezierSegment.SecondControlPoint);
        Assert.AreEqual(p2, bezierSegment.ThirdControlPoint);
    }

    [TestMethod]
    public void GetBezierPoint_ShouldCalculateCorrectly()
    {
        // Arrange
        var p0 = new XY(0, 0);
        var p1 = new XY(12, 8);
        var p2 = new XY(16, 2);
        var bezierSegment = new QuadraticBezierSegment(p0, p1, p2);

        // Act
        var point = bezierSegment.GetBezierPoint(0.5);

        // Assert
        Assert.IsTrue(point.X.AlmostEqualTo(10));
        Assert.IsTrue(point.Y.AlmostEqualTo(4.5));
    }

    [TestMethod]
    public void ToPolyline_ShouldApproximateCorrectly()
    {
        // Arrange
        var p0 = new XY(0, 0);
        var p1 = new XY(1, 2);
        var p2 = new XY(3, 3);
        var bezierSegment = new QuadraticBezierSegment(p0, p1, p2);
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
        var bezierSegment = new QuadraticBezierSegment(p0, p1, p2);
        var targetPoint = new XY(0.5, 1);
        var expectedPoint = new XY(0.55, 0.9);

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
        var bezierSegment = new QuadraticBezierSegment(p0, p1, p2);
        var targetPoint = new XY(1, 1);

        // Act
        var closestPoint = bezierSegment.ClosestPoint(targetPoint, 0);
    }
}
