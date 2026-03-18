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
    private Length InternalRadius => _internalDiameter / 2;

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

        IThicknessCalculator calculator = type switch
            {
                StressType.Circumferential => IsThickShell(type) 
                    ? new CircumferentialThickShellCalculator() 
                    : new CircumferentialThinShellCalculator(),
                
                StressType.Longitudinal => IsThickShell(type)
                    ? new LongitudinalThickShellCalculator()
                    : new LongitudinalThinShellCalculator(),
                    
                _ => throw new ArgumentOutOfRangeException(nameof(type))
            };
            
        return calculator.CalculateThickness(EffectivePressure, InternalRadius, _allowableStress, _jointEfficiency);
    }
        
    private bool IsThickShell(StressType type) 
        {
            double limit = type == StressType.Circumferential ? 0.385 : 1.25;
            return EffectivePressure > limit * _allowableStress * _jointEfficiency;
        }


}
