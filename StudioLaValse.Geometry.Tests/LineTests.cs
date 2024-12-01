using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StudioLaValse.Geometry.Tests
{
    [TestClass]
    public class LineTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeWithPointsCorrectly()
        {
            // Arrange
            var start = new XY(1, 1);
            var end = new XY(4, 5);

            // Act
            var line = new Line(start, end);

            // Assert
            Assert.AreEqual(start, line.Start);
            Assert.AreEqual(end, line.End);
        }

        [TestMethod]
        public void Constructor_ShouldInitializeWithCoordinatesCorrectly()
        {
            // Arrange
            double x1 = 1, y1 = 1, x2 = 4, y2 = 5;

            // Act
            var line = new Line(x1, y1, x2, y2);

            // Assert
            Assert.AreEqual(new XY(x1, y1), line.Start);
            Assert.AreEqual(new XY(x2, y2), line.End);
        }

        [TestMethod]
        public void Distance_ShouldCalculateCorrectly()
        {
            // Arrange
            var line = new Line(new XY(0, 0), new XY(5, 5));
            var point = new XY(3, 0);

            // Act
            var distance = line.Distance(point);

            // Assert
            Assert.AreEqual(2.1213, distance, 0.0001);
        }

        [TestMethod]
        public void ClosestPoint_ShouldCalculateCorrectly()
        {
            // Arrange
            var line = new Line(new XY(0, 0), new XY(5, 5));
            var point = new XY(3, 0);

            // Act
            var closestPoint = line.ClosestPoint(point);

            // Assert
            Assert.AreEqual(new XY(1.5, 1.5), closestPoint);
        }

        [TestMethod]
        public void ClosestPoint_PointOnLine_ShouldReturnSamePoint()
        {
            // Arrange
            var line = new Line(new XY(0, 0), new XY(5, 5));
            var point = new XY(2, 2);

            // Act
            var closestPoint = line.ClosestPoint(point);

            // Assert
            Assert.AreEqual(point, closestPoint);
        }

        [TestMethod]
        public void ClosestPoint_PointAtStart_ShouldReturnStart()
        {
            // Arrange
            var line = new Line(new XY(0, 0), new XY(5, 5));
            var point = new XY(-1, -1);

            // Act
            var closestPoint = line.ClosestPoint(point);

            // Assert
            Assert.AreEqual(line.Start, closestPoint);
        }

        [TestMethod]
        public void ClosestPoint_PointAtEnd_ShouldReturnEnd()
        {
            // Arrange
            var line = new Line(new XY(0, 0), new XY(5, 5));
            var point = new XY(6, 6);

            // Act
            var closestPoint = line.ClosestPoint(point);

            // Assert
            Assert.AreEqual(line.End, closestPoint);
        }
    }
}
