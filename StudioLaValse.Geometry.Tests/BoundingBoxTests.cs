namespace StudioLaValse.Geometry.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

[TestClass]
public class BoundingBoxTests
{
    [TestMethod]
    public void Constructor_WithDoubles_ShouldInitializeCorrectly()
    {
        // Arrange
        double x1 = 1, x2 = 3, y1 = 2, y2 = 4;

        // Act
        var boundingBox = new BoundingBox(x1, x2, y1, y2);

        // Assert
        Assert.AreEqual(new XY(1, 2), boundingBox.MinPoint);
        Assert.AreEqual(new XY(3, 4), boundingBox.MaxPoint);
    }

    [TestMethod]
    public void Constructor_WithPoints_ShouldInitializeCorrectly()
    {
        // Arrange
        var point1 = new XY(1, 2);
        var point2 = new XY(3, 4);

        // Act
        var boundingBox = new BoundingBox(point1, point2);

        // Assert
        Assert.AreEqual(point1, boundingBox.MinPoint);
        Assert.AreEqual(point2, boundingBox.MaxPoint);
    }

    [TestMethod]
    public void Constructor_WithBoundingBoxes_ShouldInitializeCorrectly()
    {
        // Arrange
        var boxes = new List<BoundingBox>
        {
            new BoundingBox(0, 2, 1, 3),
            new BoundingBox(2, 4, 3, 5)
        };

        // Act
        var boundingBox = new BoundingBox(boxes);

        // Assert
        Assert.AreEqual(new XY(0, 1), boundingBox.MinPoint);
        Assert.AreEqual(new XY(4, 5), boundingBox.MaxPoint);
    }

    [TestMethod]
    public void PropertyValues_ShouldBeCorrect()
    {
        // Arrange
        var boundingBox = new BoundingBox(1, 3, 2, 4);

        // Act & Assert
        Assert.AreEqual(new XY(1, 2), boundingBox.MinPoint);
        Assert.AreEqual(new XY(3, 4), boundingBox.MaxPoint);
        Assert.AreEqual(new XY(2, 3), boundingBox.Center);
        Assert.AreEqual(2, boundingBox.Height);
        Assert.AreEqual(2, boundingBox.Width);
    }

    [TestMethod]
    public void ClosestPointShape_PointInside_ShouldReturnSamePoint()
    {
        // Arrange
        var boundingBox = new BoundingBox(1, 5, 2, 6);
        var point = new XY(3, 4);

        // Act
        var closestPoint = boundingBox.ClosestPointShape(point);

        // Assert
        Assert.AreEqual(point, closestPoint);
    }

    [TestMethod]
    public void ClosestPointShape_PointOutside_ShouldReturnClosestPointInside()
    {
        // Arrange
        var boundingBox = new BoundingBox(1, 5, 2, 6);
        var point = new XY(6, 7);

        // Act
        var closestPoint = boundingBox.ClosestPointShape(point);

        // Assert
        Assert.AreEqual(new XY(5, 6), closestPoint);
    }

    [TestMethod]
    public void ClosestPointEdge_PointInside_ShouldReturnClosestEdgePoint()
    {
        // Arrange
        var boundingBox = new BoundingBox(0, 8, 0, 6);
        var point = new XY(1, 2);

        // Act
        var closestPoint = boundingBox.ClosestPointEdge(point);

        // Assert
        Assert.AreEqual(new XY(0, 2), closestPoint); // Expected: closest point on the bottom edge
    }


    [TestMethod]
    public void ClosestPointEdge_PointOutside_ShouldReturnClosestEdgePoint()
    {
        // Arrange
        var boundingBox = new BoundingBox(1, 5, 2, 6);
        var point = new XY(6, 7);

        // Act
        var closestPoint = boundingBox.ClosestPointEdge(point);

        // Assert
        Assert.AreEqual(new XY(5, 6), closestPoint);
    }

    [TestMethod]
    public void ClosestPointEdge_PointOnEdge_ShouldReturnSamePoint()
    {
        // Arrange
        var boundingBox = new BoundingBox(1, 5, 2, 6);
        var point = new XY(1, 4);

        // Act
        var closestPoint = boundingBox.ClosestPointEdge(point);

        // Assert
        Assert.AreEqual(point, closestPoint);
    }


}
