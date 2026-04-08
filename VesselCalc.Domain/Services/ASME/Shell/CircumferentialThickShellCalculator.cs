namespace VesselCalc.Domain;

using UnitsNet;
using UnitsNet.Units;

internal class CircumferentialThickShellCalculator : ShellCalculatorBase
{
    public override Length CalculateThickness(Pressure effectivePressure, Length internalRadius, Pressure allowableStress, double jointEfficiency)
    {
        var (P, R, S, E) = GetConstants(effectivePressure, internalRadius, allowableStress, jointEfficiency);

        double Z = (S * E + P) / (S * E - P);
        
        double thickness = R * (Math.Sqrt(Z) - 1);

        return Length.FromMillimeters(thickness);
    }
}