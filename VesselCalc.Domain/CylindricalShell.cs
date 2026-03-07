namespace VesselCalc.Domain;
using UnitsNet;
using UnitsNet.Units;

public class CylindricalShell
{
    private readonly Pressure _designPressure;
    private readonly Length _internalDiameter;
    private readonly double _jointEfficiency;
    private readonly Pressure _allowableStress;
    private readonly Pressure _fluidColumnPressure;

    private Pressure EffectivePressure => _designPressure + _fluidColumnPressure;

    public CylindricalShell(Pressure designPressure, Length internalDiameter, Pressure allowableStress, double jointEfficiency, Pressure fluidColumnPressure)
    {
        _designPressure = designPressure;
        _internalDiameter = internalDiameter;
        _allowableStress = allowableStress;
        _jointEfficiency = jointEfficiency;
        _fluidColumnPressure = fluidColumnPressure;
    }
    
    public CylindricalShell(Pressure designPressure, Length internalDiameter, Pressure allowableStress, double jointEfficiency, Density densityOfFluid, Length heightOfFluidColumn)
        : this(designPressure, internalDiameter, allowableStress, jointEfficiency, Pressure.FromKilogramsForcePerSquareCentimeter((densityOfFluid.GramsPerCubicCentimeter * heightOfFluidColumn.Meters) / 10))
    {
    }

    public Length CalculateThickness(StressType type)
    {

        switch (type)
        {
            case StressType.Circumferential when EffectivePressure <= 0.385 * _allowableStress * _jointEfficiency:
                return CalculateThickness_CircumferentialThinShell();
            case StressType.Circumferential when EffectivePressure > 0.385 * _allowableStress * _jointEfficiency:
                return CalculateThickness_CircumferentialThickShell();
            case StressType.Longitudinal when EffectivePressure <= 1.25 * _allowableStress * _jointEfficiency:
                return CalculateThickness_LongitudinalThinShell();
            case StressType.Longitudinal when EffectivePressure > 1.25 * _allowableStress * _jointEfficiency:
                return CalculateThickness_LongitudinalThickShell();
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, "Condição de tensão não mapeada pela norma.");
        }
        
    }

    public Length CalculateThickness_CircumferentialThinShell()
    {
        double p = EffectivePressure.KilogramsForcePerSquareCentimeter;
        double r = _internalDiameter.Millimeters / 2;
        double s = _allowableStress.KilogramsForcePerSquareCentimeter;
        double e = _jointEfficiency;

        double thickness = (p * r) / ((s * e) - (0.6 * p));
        
        return Length.FromMillimeters(thickness);
    }

    public Length CalculateThickness_CircumferentialThickShell()
    {
        double p = EffectivePressure.KilogramsForcePerSquareCentimeter;
        double r = _internalDiameter.Millimeters / 2;
        double s = _allowableStress.KilogramsForcePerSquareCentimeter;
        double e = _jointEfficiency;

        double z = (s * e + p) / (s * e - p);
        double thickness = r * (Math.Sqrt(z) - 1);

        return (Length.FromMillimeters(thickness));
    }

    public Length CalculateThickness_LongitudinalThinShell()
    {
        double p = EffectivePressure.KilogramsForcePerSquareCentimeter;
        double r = _internalDiameter.Millimeters / 2;
        double s = _allowableStress.KilogramsForcePerSquareCentimeter;
        double e = _jointEfficiency;

        double thickness = (p * r) / ((2 * s * e) + (0.4 * p));
        
        return Length.FromMillimeters(thickness);
    }

    public Length CalculateThickness_LongitudinalThickShell()
    {
        double p = EffectivePressure.KilogramsForcePerSquareCentimeter;
        double r = _internalDiameter.Millimeters / 2;
        double s = _allowableStress.KilogramsForcePerSquareCentimeter;
        double e = _jointEfficiency;

        double z = (p / (s * e)) + 1;
        double thickness = r * (Math.Sqrt(z) - 1);
        
        return Length.FromMillimeters(thickness);
    }
}
