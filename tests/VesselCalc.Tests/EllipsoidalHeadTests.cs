using System;
using UnitsNet;
using Xunit;
using VesselCalc.Domain;

namespace VesselCalc.Tests;

public class EllipsoidalHeadTests
{

    [Fact]
    public void CalculateMinimumRequiredThickness_DefaultRatio_ShouldReturnCorrectThickness()
    {
        // CENÁRIO: Tampo Elipsoidal padrão 2:1 sob pressão interna (ASME VIII Div.1, UG-32(c))
        // P_eff = 2.0 MPa, D = 2000 mm, S = 138 MPa, E = 0.85, CA = 3.0 mm.
        // t_r = (2.0 * 2000) / (2 * 138 * 0.85 - 0.2 * 2.0) = 17.079 mm. 
        // t_final = 17.079 + 3.0 = 20.079 mm.
        
        // Arrange
        var designPressure = Pressure.FromMegapascals(1.5);
        var internalDiameter = Length.FromMillimeters(2000.0);
        var allowableStress = Pressure.FromMegapascals(138.0);
        var allowableCorrosion = Length.FromMillimeters(3.0);
        var jointEfficiency = 0.85;
        var fluidColumnPressure = Pressure.FromMegapascals(0.5);
        
        var expectedThickness = Length.FromMillimeters(20.079);

        var ellipsoidalHead = new EllipsoidalHead(
            designPressure,
            internalDiameter,
            allowableStress,
            allowableCorrosion,
            jointEfficiency,
            fluidColumnPressure);

        // Act
        var actualThickness = ellipsoidalHead.CalculateMinimumRequiredThickness();

        // Assert 
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }

    [Fact]
    public void CalculateMinimumRequiredThickness_WithHeadHeightInput_ShouldReturnCorrectThickness()
    {        
        // Arrange
        var designPressure = Pressure.FromMegapascals(1.5);
        var internalDiameter = Length.FromMillimeters(2000.0);
        var allowableStress = Pressure.FromMegapascals(138.0);
        var allowableCorrosion = Length.FromMillimeters(3.0);
        var jointEfficiency = 0.85;
        var fluidColumnPressure = Pressure.FromMegapascals(0.5);
        var headHeight = Length.FromMillimeters(300.0); 
        
        var expectedThickness = Length.FromMillimeters(40.322);

        var ellipsoidalHead = new EllipsoidalHead(
            designPressure,
            internalDiameter,
            allowableStress,
            allowableCorrosion,
            jointEfficiency,
            fluidColumnPressure,
            headHeight);

        // Act
        var actualThickness = ellipsoidalHead.CalculateMinimumRequiredThickness();

        // Assert 
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }

    [Fact]
    public void CalculateMinimumRequiredThickness_WithFactorKInput_ShouldReturnCorrectThickness()
    {        
        // Arrange
        var designPressure = Pressure.FromMegapascals(1.5);
        var internalDiameter = Length.FromMillimeters(2000.0);
        var allowableStress = Pressure.FromMegapascals(138.0);
        var allowableCorrosion = Length.FromMillimeters(3.0);
        var jointEfficiency = 0.85;
        var fluidColumnPressure = Pressure.FromMegapascals(0.5);
        var factorK = 2; 
        
        var expectedThickness = Length.FromMillimeters(37.159);

        var ellipsoidalHead = new EllipsoidalHead(
            designPressure,
            internalDiameter,
            allowableStress,
            allowableCorrosion,
            jointEfficiency,
            fluidColumnPressure,
            null,
            factorK);

        // Act
        var actualThickness = ellipsoidalHead.CalculateMinimumRequiredThickness();

        // Assert 
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }

    [Fact]
    public void CalculateMinimumRequiredThickness_WithHeadHeightAndFactorKInput_ShouldReturnCorrectThickness()
    {        
        // Arrange
        var designPressure = Pressure.FromMegapascals(1.5);
        var internalDiameter = Length.FromMillimeters(2000.0);
        var allowableStress = Pressure.FromMegapascals(138.0);
        var allowableCorrosion = Length.FromMillimeters(3.0);
        var jointEfficiency = 0.85;
        var fluidColumnPressure = Pressure.FromMegapascals(0.5);
        var headHeight = Length.FromMillimeters(300.0);
        var factorK = 2;

        var expectedThickness = Length.FromMillimeters(37.159);

        var ellipsoidalHead = new EllipsoidalHead(
            designPressure,
            internalDiameter,
            allowableStress,
            allowableCorrosion,
            jointEfficiency,
            fluidColumnPressure,
            headHeight,
            factorK);

        // Act
        var actualThickness = ellipsoidalHead.CalculateMinimumRequiredThickness();

        // Assert 
        Assert.Equal(expectedThickness.Millimeters, actualThickness.Millimeters, 3);
    }
}