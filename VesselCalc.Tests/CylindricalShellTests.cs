using Xunit;
using UnitsNet;
using UnitsNet.Units;
using VesselCalc.Domain;

namespace VesselCalc.Tests;

public class CylindricalShellTests
{
    [Fact]
    public void CalculateThickness_ThinShellCircumferentialStress_ShouldReturnCorrectValue()
    {
        // Arrange
        Pressure designPressure = Pressure.FromKilogramsForcePerSquareCentimeter(100);
        Length internalDiameter = Length.FromMillimeters(2000);
        double jointEfficiency = 1;
        Pressure allowableStress = Pressure.FromKilogramsForcePerSquareCentimeter(1000);
        Pressure fluidColumnPressure = Pressure.FromKilogramsForcePerSquareCentimeter(0);
        Length expectedThickness = Length.FromMillimeters(106.383);

        // Act
        var shell = new CylindricalShell(designPressure, internalDiameter, allowableStress, jointEfficiency, fluidColumnPressure);
        var actualThickness = shell.CalculateThickness(StressType.Circumferential);

        // Assert
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }

    [Fact]
    public void CalculateThickness_ThinShellLongitudinalStress_ShouldReturnCorrectValue()
    {
        // Arrange
        Pressure designPressure = Pressure.FromKilogramsForcePerSquareCentimeter(100);
        Length internalDiameter = Length.FromMillimeters(2000);
        double jointEfficiency = 1;
        Pressure allowableStress = Pressure.FromKilogramsForcePerSquareCentimeter(1000);
        Pressure fluidColumnPressure = Pressure.FromKilogramsForcePerSquareCentimeter(0);
        Length expectedThickness = Length.FromMillimeters(49.020);

        // Act
        var shell = new CylindricalShell(designPressure, internalDiameter, allowableStress, jointEfficiency, fluidColumnPressure);
        var actualThickness = shell.CalculateThickness(StressType.Longitudinal);

        // Assert
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }


    [Fact]
    public void CalculateThickness_ThickShellCircumferentialStress_ShouldReturnCorrectValue()
    {
        // Arrange
        Pressure designPressure = Pressure.FromKilogramsForcePerSquareCentimeter(500);
        Length internalDiameter = Length.FromMillimeters(2000);
        double jointEfficiency = 1;
        Pressure allowableStress = Pressure.FromKilogramsForcePerSquareCentimeter(1000);
        Pressure fluidColumnPressure = Pressure.FromKilogramsForcePerSquareCentimeter(0);
        Length expectedThickness = Length.FromMillimeters(732.051);

        // Act
        var shell = new CylindricalShell(designPressure, internalDiameter, allowableStress, jointEfficiency, fluidColumnPressure);
        var actualThickness = shell.CalculateThickness(StressType.Circumferential);

        // Assert
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }

    [Fact]
    public void CalculateThickness_ThickShellLongitudinalStress_ShouldReturnCorrectValue()
    {
        // Arrange
        Pressure designPressure = Pressure.FromKilogramsForcePerSquareCentimeter(1500);
        Length internalDiameter = Length.FromMillimeters(2000);
        double jointEfficiency = 1;
        Pressure allowableStress = Pressure.FromKilogramsForcePerSquareCentimeter(1000);
        Pressure fluidColumnPressure = Pressure.FromKilogramsForcePerSquareCentimeter(0);
        Length expectedThickness = Length.FromMillimeters(581.139);

        // Act
        var shell = new CylindricalShell(designPressure, internalDiameter, allowableStress, jointEfficiency, fluidColumnPressure);
        var actualThickness = shell.CalculateThickness(StressType.Longitudinal);

        // Assert
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }
}
