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
public class PolylineTests
{
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void Constructor_ShouldThrowException_WhenNoPointsGiven()
    {
        // Arrange
        var points = new List<XY>();

        // Act
        var polyline = new Polyline(points);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeCorrectly_WhenValidPointsGiven()
    {
        // Arrange
        var points = new List<XY> { new XY(1, 1), new XY(2, 2), new XY(3, 1) };

        // Act
        var polyline = new Polyline(points);

        // Assert
        Assert.AreEqual(3, polyline.Points.Count);
    }

    [TestMethod]
    public void TotalLength_ShouldCalculateCorrectly()
    {
        // Arrange
        var points = new List<XY> { new XY(0, 0), new XY(3, 4) }; // Length should be 5 (3-4-5 triangle)
        var polyline = new Polyline(points);

        // Act
        var totalLength = polyline.TotalLength();

        // Assert
        Assert.AreEqual(5, totalLength);
    }

    [TestMethod]
    public void ClosestPoint_ShouldFindClosestPoint()
    {
        // Arrange
        var points = new List<XY> { new XY(0, 0), new XY(5, 0), new XY(5, 5) };
        var polyline = new Polyline(points);
        var targetPoint = new XY(3, 2);

        // Act
        var closestPoint = polyline.ClosestPoint(targetPoint);

        // Assert
        Assert.AreEqual(new XY(3, 0), closestPoint);
    }

    [TestMethod]
    public void GetLineSegments_ShouldReturnCorrectSegments()
    {
        // Arrange
        var points = new List<XY> { new XY(1, 1), new XY(2, 2), new XY(3, 1) };
        var polyline = new Polyline(points);

        // Act
        var segments = polyline.GetLineSegments();

        // Assert
        var expectedSegments = new List<Line>
        {
            new Line(new XY(1, 1), new XY(2, 2)),
            new Line(new XY(2, 2), new XY(3, 1))
        };

        CollectionAssert.AreEqual(expectedSegments, new List<Line>(segments));
    }
}
