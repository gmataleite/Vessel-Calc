namespace VesselCalc.Domain;

using UnitsNet;
using UnitsNet.Units;

internal class CircumferentialThickShellCalculator : ShellCalculatorBase
{
    public override Length CalculateThickness(Pressure p, Length r, Pressure s, double e)
    {
        var (P, R, S, E) = GetConstants(p, r, s, e);

        double z = (S * E + P) / (S * E - P);
        
        double thickness = R * (Math.Sqrt(z) - 1);

        return Length.FromMillimeters(thickness);
    }
}