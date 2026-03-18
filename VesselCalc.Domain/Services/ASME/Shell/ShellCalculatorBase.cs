namespace VesselCalc.Domain;

using UnitsNet;
using UnitsNet.Units;

public abstract class ShellCalculatorBase : IThicknessCalculator
{
    protected (double p, double r, double s, double e) GetConstants(Pressure effectivePressure, Length internalRadius, Pressure allowableStress, double jointEfficiency)
    {
        return (
            effectivePressure.Pascals, 
            internalRadius.Millimeters, 
            allowableStress.Pascals, 
            jointEfficiency
        );
    }
    public abstract Length CalculateThickness(Pressure p, Length r, Pressure s, double e);

}