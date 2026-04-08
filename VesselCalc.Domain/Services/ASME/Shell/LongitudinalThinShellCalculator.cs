namespace VesselCalc.Domain;

using UnitsNet;
using UnitsNet.Units;

internal class LongitudinalThinShellCalculator : ShellCalculatorBase
{
    public override Length CalculateThickness(Pressure effectivePressure, Length internalRadius, Pressure allowableStress, double jointEfficiency)
    {
        var (P, R, S, E) = GetConstants(effectivePressure, internalRadius, allowableStress, jointEfficiency);

        double thickness = (P * R) / ((2 * S * E) + (0.4 * P));
        
        return Length.FromMillimeters(thickness);
    }
}