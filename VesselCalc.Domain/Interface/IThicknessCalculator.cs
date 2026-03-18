using UnitsNet;

namespace VesselCalc.Domain;

public interface IThicknessCalculator
{
    Length CalculateThickness(Pressure effectivePressure, Length internalRadius, Pressure allowableStress, double jointEfficiency);
}
