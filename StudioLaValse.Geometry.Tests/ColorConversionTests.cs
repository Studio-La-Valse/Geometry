using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.Geometry.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ColorConversionTests
{
    [TestMethod]
    public void ToColorAHSV_FromColorARGB_ShouldConvertCorrectly()
    {
        // Arrange
        var colorARGB = new ColorARGB(1.0, 120, 65, 210);

        // Act
        var colorAHSV = colorARGB.ToColorAHSV();

        // Assert
        Assert.AreEqual(colorARGB.Alpha, colorAHSV.Alpha);
        Assert.AreEqual(263, colorAHSV.Hue); // Expected hue value based on calculations
        Assert.AreEqual(69, colorAHSV.Saturation); // Expected saturation value based on calculations
        Assert.AreEqual(82, colorAHSV.Value); // Expected brightness value based on calculations
    }

    [TestMethod]
    public void ToColorARGB_FromColorAHSV_ShouldConvertCorrectly()
    {
        // Arrange
        var colorAHSV = new ColorAHSV(1.0, 262, 69, 82);

        // Act
        var colorARGB = colorAHSV.ToColorARGB();

        // Assert
        Assert.AreEqual(colorAHSV.Alpha, colorARGB.Alpha);
        Assert.AreEqual(118, colorARGB.Red); // Expected red value based on calculations
        Assert.AreEqual(65, colorARGB.Green); // Expected green value based on calculations
        Assert.AreEqual(209, colorARGB.Blue); // Expected blue value based on calculations
    }

    [TestMethod]
    public void ToColorAHSL_FromColorARGB_ShouldConvertCorrectly()
    {
        // Arrange
        var colorARGB = new ColorARGB(1.0, 120, 65, 210);

        // Act
        var colorAHSL = colorARGB.ToColorAHSL();

        // Assert
        Assert.AreEqual(colorARGB.Alpha, colorAHSL.Alpha);
        Assert.AreEqual(263, colorAHSL.Hue); // Expected hue value based on calculations
        Assert.AreEqual(62, colorAHSL.Saturation); // Expected saturation value based on calculations
        Assert.AreEqual(54, colorAHSL.Lightness); // Expected lightness value based on calculations
    }

    [TestMethod]
    public void ToColorARGB_FromColorAHSL_ShouldConvertCorrectly()
    {
        // Arrange
        var colorAHSL = new ColorAHSL(1.0, 262, 61, 53);

        // Act
        var colorARGB = colorAHSL.ToColorARGB();

        // Assert
        Assert.AreEqual(colorAHSL.Alpha, colorARGB.Alpha);
        Assert.AreEqual(116, colorARGB.Red); // Expected red value based on calculations
        Assert.AreEqual(62, colorARGB.Green); // Expected green value based on calculations
        Assert.AreEqual(208, colorARGB.Blue); // Expected blue value based on calculations
    }

    [TestMethod]
    public void ToColorAHSL_FromColorAHSV_ShouldConvertCorrectly()
    {
        // Arrange
        var colorAHSV = new ColorAHSV(1.0, 226, 69, 82);

        // Act
        var colorAHSL = colorAHSV.ToColorAHSL();

        // Assert
        Assert.AreEqual(colorAHSV.Alpha, colorAHSL.Alpha);
        Assert.AreEqual(colorAHSV.Hue, colorAHSL.Hue); // Hue should remain the same
        Assert.AreEqual(61, colorAHSL.Saturation); // Expected saturation value based on calculations
        Assert.AreEqual(54, colorAHSL.Lightness); // Expected lightness value based on calculations
    }

    [TestMethod]
    public void ToColorAHSV_FromColorAHSL_ShouldConvertCorrectly()
    {
        // Arrange
        var colorAHSL = new ColorAHSL(1.0, 226, 61, 53);

        // Act
        var colorAHSV = colorAHSL.ToColorAHSV();

        // Assert
        Assert.AreEqual(colorAHSL.Alpha, colorAHSV.Alpha);
        Assert.AreEqual(colorAHSL.Hue, colorAHSV.Hue); // Hue should remain the same
        Assert.AreEqual(70, colorAHSV.Saturation); // Expected saturation value based on calculations
        Assert.AreEqual(82, colorAHSV.Value); // Expected brightness value based on calculations
    }

    [TestMethod]
    public void ToColorARGB_FromColorAHSV_TestCase1()
    {
        // Arrange
        var colorAHSV = new ColorAHSV(1.0, 0, 100, 50);

        // Act
        var colorARGB = colorAHSV.ToColorARGB();

        // Assert
        Assert.AreEqual(128, colorARGB.Red);
        Assert.AreEqual(0, colorARGB.Green);
        Assert.AreEqual(0, colorARGB.Blue);
    }

    [TestMethod]
    public void ToColorARGB_FromColorAHSV_TestCase2()
    {
        // Arrange
        var colorAHSV = new ColorAHSV(1.0, 120, 100, 50);

        // Act
        var colorARGB = colorAHSV.ToColorARGB();

        // Assert
        Assert.AreEqual(0, colorARGB.Red);
        Assert.AreEqual(128, colorARGB.Green);
        Assert.AreEqual(0, colorARGB.Blue);
    }

    [TestMethod]
    public void ToColorARGB_FromColorAHSV_TestCase3()
    {
        // Arrange
        var colorAHSB = new ColorAHSV(1.0, 240, 100, 50);

        // Act
        var colorARGB = colorAHSB.ToColorARGB();

        // Assert
        Assert.AreEqual(0, colorARGB.Red);
        Assert.AreEqual(0, colorARGB.Green);
        Assert.AreEqual(128, colorARGB.Blue);
    }

    [TestMethod]
    public void ToColorARGB_FromColorAHSB_TestCase4()
    {
        // Arrange
        var colorAHSB = new ColorAHSV(1.0, 360, 100, 50);

        // Act
        var colorARGB = colorAHSB.ToColorARGB();

        // Assert
        Assert.AreEqual(128, colorARGB.Red);
        Assert.AreEqual(0, colorARGB.Green);
        Assert.AreEqual(0, colorARGB.Blue);
    }
}
