using System;
using System.Collections.Generic;
using UnitsNet;
using Xunit;
using VesselCalc.Domain;

namespace VesselCalc.Tests;

public class CylindricalShellTests
{
    public static IEnumerable<object[]> ValidShellTestData => new List<object[]>
    {
        // CENÁRIO 1: Casco Fino (Thin Shell) - Governado por Tensão Circunferencial (UG-27)
        // P = 2 MPa, S = 138 MPa, CA = 3 mm. P < 0.385*SE (2 < 53.13).
        // tr = (2 * 1000) / (138*1 - 0.6*2) = 14.6198 mm. Total com CA = 17.620 mm.
        new object[] { 2.0, 2000.0, 138.0, 3.0, 1.0, 0.0, 17.620 },

        // CENÁRIO 2: Casco Espesso (Thick Shell) - Pelo Limite de Pressão (Appendix 1-2)
        // P = 60 MPa, S = 138 MPa, CA = 0 mm. P > 0.385*SE (60 > 53.13). Gatilho acionado.
        // Z = (138 + 60) / (138 - 60) = 2.5384. tr = 1000 * (sqrt(2.5384) - 1) = 593.255 mm.
        new object[] { 60.0, 2000.0, 138.0, 0.0, 1.0, 0.0, 593.255 },

        // CENÁRIO 3: Casco Fino com Pressão de Coluna de Fluido (UG-22)
        // P_projeto = 1.5 MPa. P_fluido = 0.098 MPa. P_efetivo = 1.598 MPa. E = 0.85 (RT Parcial).
        // tr = (1.598 * 1000) / (138*0.85 - 0.6*1.598) = 13.736 mm. 
        new object[] { 1.5, 2000.0, 138.0, 0.0, 0.85, 0.098, 13.735 }
    };

    [Theory]
    [MemberData(nameof(ValidShellTestData))]
    public void CalculateMinimumRequiredThickness_ValidInputs_ShouldReturnCorrectThickness(
        double designPressureMpa,
        double internalDiameterMillimeters,
        double allowableStressMpa,
        double allowableCorrosionMillimeters,
        double jointEfficiency,
        double fluidColumnPressureMpa,
        double expectedThicknessMillimeters)
    {
        // Arrange
        var designPressure = Pressure.FromMegapascals(designPressureMpa);
        var internalDiameter = Length.FromMillimeters(internalDiameterMillimeters);
        var allowableStress = Pressure.FromMegapascals(allowableStressMpa);
        var allowableCorrosion = Length.FromMillimeters(allowableCorrosionMillimeters);
        var fluidColumnPressure = Pressure.FromMegapascals(fluidColumnPressureMpa);

        var shell = new CylindricalShell(
            designPressure,
            internalDiameter,
            allowableStress,
            allowableCorrosion,
            jointEfficiency,
            fluidColumnPressure);

        // Act
        var actualThickness = shell.CalculateMinimumRequiredThickness();

        // Assert 
        Assert.Equal(expectedThicknessMillimeters, actualThickness.Millimeters, 3);
    }

    [Fact]
    public void CalculateMinimumRequiredThickness_PressureExceedsAllowableStress_ShouldThrowInvalidOperationException()
    {
        // Arrange: Condição de Ruptura onde P >= S * E (Fisicamente impossível sob teoria elástica)
        var shell = new CylindricalShell(
            designPressure: Pressure.FromMegapascals(150.0),
            internalDiameter: Length.FromMillimeters(2000.0),
            allowableStress: Pressure.FromMegapascals(138.0),
            allowableCorrosion: Length.FromMillimeters(0),
            jointEfficiency: 1.0,
            fluidColumnPressure: Pressure.FromMegapascals(0)
        );

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => shell.CalculateMinimumRequiredThickness());
        Assert.Contains("P >= SE", exception.Message);
    }
}