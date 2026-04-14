namespace VesselCalc.Domain;

using System;
using VesselCalc.Domain.Common;
using UnitsNet;

public class TorisphericalHead
{
    private readonly Pressure _designPressure;
    private readonly Length _crownRadius;
    private readonly Length _knuckleRadius;
    private readonly double _jointEfficiency;
    private readonly Pressure _allowableStress;
    private readonly Length _allowableCorrosion;
    private readonly Pressure _fluidColumnPressure;
    private readonly double _factorM;
        
    private Pressure EffectivePressure => _designPressure + _fluidColumnPressure;

// CONSTRUTOR 1: Tampo Customizado (Recebe L e r explicitamente)
    public TorisphericalHead(
        Pressure designPressure, 
        Length crownRadius, 
        Length knuckleRadius, 
        Pressure allowableStress, 
        Length allowableCorrosion, 
        double jointEfficiency, 
        Pressure fluidColumnPressure)
    {
        Guard.AgainstNegativeOrZero(designPressure, nameof(designPressure));
        Guard.AgainstNegativeOrZero(crownRadius, nameof(crownRadius));
        Guard.AgainstNegativeOrZero(knuckleRadius, nameof(knuckleRadius));
        Guard.AgainstNegativeOrZero(allowableStress, nameof(allowableStress));
        Guard.AgainstNegative(allowableCorrosion, nameof(allowableCorrosion));
        Guard.AgainstNegative(fluidColumnPressure, nameof(fluidColumnPressure));
        Guard.AgainstInvalidJointEfficiency(jointEfficiency, nameof(jointEfficiency));

        // TODO: [Business Rule] Validar limites do ASME UG-32(e). 
        // 1. O raio da junta (r) não pode ser menor que 6% do raio da coroa (L).
        // 2. O raio da coroa (L) não pode exceder o diâmetro externo da saia do tampo.
        /* if (knuckleRadius.Millimeters < 0.06 * crownRadius.Millimeters)
            throw new ArgumentOutOfRangeException(nameof(knuckleRadius), "O raio da junta (r) não pode ser menor que 6% do raio da coroa (L) conforme UG-32(e).");
        */
        // 3. Implementar ratio = 0.1 (padrão)

        _designPressure = designPressure;
        _crownRadius = crownRadius;
        _knuckleRadius = knuckleRadius;
        _allowableStress = allowableStress;
        _allowableCorrosion = allowableCorrosion;
        _jointEfficiency = jointEfficiency;
        _fluidColumnPressure = fluidColumnPressure;

        _factorM = CalculateFactorM(_crownRadius, _knuckleRadius);
    }

    public TorisphericalHead(
        Pressure designPressure, 
        Length internalDiameter, 
        Pressure allowableStress, 
        Length allowableCorrosion, 
        double jointEfficiency, 
        Pressure fluidColumnPressure, double ratio) 
        : this(
            designPressure, 
            crownRadius: internalDiameter,                       // L = D
            knuckleRadius: internalDiameter * ratio,             // r = 6% de D
            allowableStress, 
            allowableCorrosion, 
            jointEfficiency, 
            fluidColumnPressure)
    {
        Guard.AgainstNegativeOrZero(internalDiameter, nameof(internalDiameter));
    }

    public Length CalculateMinimumRequiredThickness() // ASME VIII Div. 1, Appendix 1
    {
        double P = EffectivePressure.Megapascals;
        double L = _crownRadius.Millimeters;
        double S = _allowableStress.Megapascals;
        double E = _jointEfficiency;
        double M = _factorM;

        double thickness = P * L * M / (2 * S * E - 0.2 * P);
        
        return Length.FromMillimeters(thickness) + _allowableCorrosion;
    }

    private double CalculateFactorM(Length crownRadius, Length knuckleRadius)
    {

        return (3.0 + Math.Sqrt(crownRadius / knuckleRadius)) / 4;
        
    }
}