using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.Geometry.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CircleTests
{
    [TestMethod]
    public void Constructor_WithCenterAndRadius_ShouldInitializeCorrectly()
    {
        // Arrange
        var center = new XY(3, 4);
        var radius = 5;

        // Act
        var circle = new Circle(center, radius);

        // Assert
        Assert.AreEqual(center, circle.Center);
        Assert.AreEqual(radius, circle.Radius);
    }

    [TestMethod]
    public void Constructor_WithCoordinatesAndRadius_ShouldInitializeCorrectly()
    {
        // Arrange
        var x = 3;
        var y = 4;
        var radius = 5;

        // Act
        var circle = new Circle(x, y, radius);

        // Assert
        Assert.AreEqual(new XY(x, y), circle.Center);
        Assert.AreEqual(radius, circle.Radius);
    }

    [TestMethod]
    public void ClosestPointShape_PointInside_ShouldReturnSamePoint()
    {
        // Arrange
        var circle = new Circle(new XY(3, 4), 5);
        var point = new XY(4, 5);

        // Act
        var closestPoint = circle.ClosestPointShape(point);

        // Assert
        Assert.AreEqual(point, closestPoint);
    }

    [TestMethod]
    public void ClosestPointShape_PointOutside_ShouldReturnClosestPointOnEdge()
    {
        // Arrange
        var circle = new Circle(new XY(3, 4), 5);
        var point = new XY(10, 10);

        // Act
        var closestPoint = circle.ClosestPointShape(point);

        // Assert
        var distance = new XY(6.796, 7.253).DistanceTo(closestPoint);
        Assert.IsTrue(distance.AlmostEqualTo(0));
    }

    [TestMethod]
    public void ClosestPointShape_PointOnEdge_ShouldReturnSamePoint()
    {
        // Arrange
        var circle = new Circle(new XY(3, 4), 5);
        var point = new XY(8, 4);

        // Act
        var closestPoint = circle.ClosestPointShape(point);

        // Assert
        Assert.AreEqual(point, closestPoint);
    }

    [TestMethod]
    public void ClosestPointEdge_PointInside_ShouldReturnClosestPointOnEdge()
    {
        // Arrange
        var circle = new Circle(new XY(3, 4), 5);
        var point = new XY(4, 5);

        // Act
        var closestPoint = circle.ClosestPointEdge(point);

        // Assert
        var distance = new XY(6.535, 7.535).DistanceTo(closestPoint);
        Assert.IsTrue(distance.AlmostEqualTo(0));
    }

    [TestMethod]
    public void ClosestPointEdge_PointOutside_ShouldReturnClosestPointOnEdge()
    {
        // Arrange
        var circle = new Circle(new XY(3, 4), 5);
        var point = new XY(10, 10);

        // Act
        var closestPoint = circle.ClosestPointEdge(point);

        // Assert
        var distance = new XY(6.796, 7.253).DistanceTo(closestPoint);
        Assert.IsTrue(distance.AlmostEqualTo(0));
    }

    [TestMethod]
    public void ClosestPointEdge_PointAtCenter_ShouldReturnPointOnEdge()
    {
        // Arrange
        var circle = new Circle(new XY(3, 4), 5);
        var point = new XY(3, 4);

        // Act
        var closestPoint = circle.ClosestPointEdge(point);

        // Assert
        Assert.AreEqual(new XY(8, 4), closestPoint);
    }

    [TestMethod]
    public void ClosestPointEdge_PointOnEdge_ShouldReturnSamePoint()
    {
        // Arrange
        var circle = new Circle(new XY(3, 4), 5);
        var point = new XY(8, 4);

        // Act
        var closestPoint = circle.ClosestPointEdge(point);

        // Assert
        Assert.AreEqual(point, closestPoint);
    }
}
