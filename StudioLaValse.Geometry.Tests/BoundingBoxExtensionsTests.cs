namespace StudioLaValse.Geometry.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class BoundingBoxExtensionsTests
{
    [TestMethod]
    public void Expand_ShouldExpandBoundingBoxCorrectly()
    {
        // Arrange
        var boundingBox = new BoundingBox(new XY(1, 1), new XY(5, 5));
        double offset = 2;

        // Act
        var expandedBox = boundingBox.Expand(offset);

        // Assert
        Assert.AreEqual(new XY(-1, -1), expandedBox.MinPoint);
        Assert.AreEqual(new XY(7, 7), expandedBox.MaxPoint);
    }

    [TestMethod]
    public void Overlaps_ShouldReturnTrue_WhenBoundingBoxesOverlap()
    {
        // Arrange
        var box1 = new BoundingBox(new XY(1, 1), new XY(5, 5));
        var box2 = new BoundingBox(new XY(4, 4), new XY(6, 6));

        // Act
        var result = box1.Overlaps(box2);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Overlaps_ShouldReturnFalse_WhenBoundingBoxesDoNotOverlap()
    {
        // Arrange
        var box1 = new BoundingBox(new XY(1, 1), new XY(3, 3));
        var box2 = new BoundingBox(new XY(4, 4), new XY(6, 6));

        // Act
        var result = box1.Overlaps(box2);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ContainedBy_ShouldReturnTrue_WhenBoundingBoxIsContained()
    {
        // Arrange
        var innerBox = new BoundingBox(new XY(2, 2), new XY(4, 4));
        var outerBox = new BoundingBox(new XY(1, 1), new XY(5, 5));

        // Act
        var result = innerBox.ContainedBy(outerBox);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ContainedBy_ShouldReturnFalse_WhenBoundingBoxIsNotContained()
    {
        // Arrange
        var innerBox = new BoundingBox(new XY(0, 0), new XY(6, 6));
        var outerBox = new BoundingBox(new XY(1, 1), new XY(5, 5));

        // Act
        var result = innerBox.ContainedBy(outerBox);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Contains_ShouldReturnTrue_WhenBoundingBoxContainsAnother()
    {
        // Arrange
        var outerBox = new BoundingBox(new XY(1, 1), new XY(5, 5));
        var innerBox = new BoundingBox(new XY(2, 2), new XY(4, 4));

        // Act
        var result = outerBox.Contains(innerBox);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Contains_ShouldReturnFalse_WhenBoundingBoxDoesNotContainAnother()
    {
        // Arrange
        var outerBox = new BoundingBox(new XY(1, 1), new XY(5, 5));
        var innerBox = new BoundingBox(new XY(0, 0), new XY(6, 6));

        // Act
        var result = outerBox.Contains(innerBox);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Contains_ShouldReturnTrue_WhenPointIsWithinBoundingBox()
    {
        // Arrange
        var boundingBox = new BoundingBox(new XY(1, 1), new XY(5, 5));
        var point = new XY(3, 3);

        // Act
        var result = boundingBox.Contains(point);

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Contains_ShouldReturnFalse_WhenPointIsOutsideBoundingBox()
    {
        // Arrange
        var boundingBox = new BoundingBox(new XY(1, 1), new XY(5, 5));
        var point = new XY(6, 6);

        // Act
        var result = boundingBox.Contains(point);

        // Assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Contains_ShouldReturnFalse_WhenPointIsOnEdgeOfBoundingBox()
    {
        // Arrange
        var boundingBox = new BoundingBox(new XY(1, 1), new XY(5, 5));
        var point = new XY(1, 3);

        // Act
        var result = boundingBox.Contains(point);

        // Assert
        Assert.IsFalse(result);
    }
}
