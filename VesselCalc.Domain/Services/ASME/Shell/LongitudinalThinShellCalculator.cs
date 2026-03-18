namespace VesselCalc.Domain;

using UnitsNet;
using UnitsNet.Units;

internal class LongitudinalThinShellCalculator : ShellCalculatorBase
{
    public override Length CalculateThickness(Pressure p, Length r, Pressure s, double e)
    {
        var (P, R, S, E) = GetConstants(p, r, s, e);

        double thickness = (P * R) / ((2 * S * E) + (0.4 * P));
        
        return Length.FromMillimeters(thickness);
    }
}