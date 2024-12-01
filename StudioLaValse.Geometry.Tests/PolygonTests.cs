using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.Geometry.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[TestClass]
public class PolygonTests
{
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Constructor_ShouldThrowException_WhenLessThanThreePoints()
    {
        // Arrange
        var points = new List<XY> { new XY(1, 1), new XY(2, 2) };

        // Act
        var polygon = new Polygon(points);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly_WhenValidPointsGiven()
    {
        // Arrange
        var points = new List<XY> { new XY(1, 1), new XY(2, 2), new XY(3, 1) };

        // Act
        var polygon = new Polygon(points);

        // Assert
        Assert.AreEqual(3, polygon.Points.Count);
    }

    [TestMethod]
    public void GetLineSegments_ShouldReturnCorrectSegments()
    {
        // Arrange
        var points = new List<XY> { new XY(1, 1), new XY(2, 2), new XY(3, 1) };
        var polygon = new Polygon(points);

        // Act
        var segments = polygon.GetLineSegments();

        // Assert
        var expectedSegments = new List<Line>
        {
            new Line(new XY(1, 1), new XY(2, 2)),
            new Line(new XY(2, 2), new XY(3, 1)),
            new Line(new XY(3, 1), new XY(1, 1))
        };

        CollectionAssert.AreEqual(expectedSegments, new List<Line>(segments));
    }

    [TestMethod]
    public void ClosestPointEdge_ShouldFindClosestPointOnEdge()
    {
        // Arrange
        var points = new List<XY> { new XY(0, 0), new XY(5, 0), new XY(5, 5), new XY(0, 5) };
        var polygon = new Polygon(points);
        var targetPoint = new XY(2, 7);

        // Act
        var closestPoint = polygon.ClosestPointEdge(targetPoint);

        // Assert
        Assert.AreEqual(new XY(2, 5), closestPoint);
    }

    [TestMethod]
    public void ClosestPointShape_ShouldFindClosestPointInside()
    {
        // Arrange
        var points = new List<XY> { new XY(0, 0), new XY(5, 0), new XY(5, 5), new XY(0, 5) };
        var polygon = new Polygon(points);
        var targetPoint = new XY(2, 2);

        // Act
        var closestPoint = polygon.ClosestPointShape(targetPoint);

        // Assert
        Assert.AreEqual(new XY(2, 2), closestPoint);
    }

    [TestMethod]
    public void IsPointInPolygon_ShouldReturnTrue_WhenPointIsInside()
    {
        // Arrange
        var points = new List<XY> { new XY(0, 0), new XY(5, 0), new XY(5, 5), new XY(0, 5) };
        var polygon = new Polygon(points);
        var targetPoint = new XY(3, 3);

        // Act
        var isInside = polygon.IsPointInPolygon(targetPoint);

        // Assert
        Assert.IsTrue(isInside);
    }

    [TestMethod]
    public void IsPointInPolygon_ShouldReturnFalse_WhenPointIsOutside()
    {
        // Arrange
        var points = new List<XY> { new XY(0, 0), new XY(5, 0), new XY(5, 5), new XY(0, 5) };
        var polygon = new Polygon(points);
        var targetPoint = new XY(6, 6);

        // Act
        var isInside = polygon.IsPointInPolygon(targetPoint);

        // Assert
        Assert.IsFalse(isInside);
    }
}
