namespace VesselCalc.Domain;

using UnitsNet;
using UnitsNet.Units;

internal class LongitudinalThickShellCalculator : ShellCalculatorBase
{
    public override Length CalculateThickness(Pressure p, Length r, Pressure s, double e)
    {
        var (P, R, S, E) = GetConstants(p, r, s, e);
        double z = (P / (S * E)) + 1;
        
        double thickness = R * (Math.Sqrt(z) - 1);
        
        return Length.FromMillimeters(thickness);
    }
}