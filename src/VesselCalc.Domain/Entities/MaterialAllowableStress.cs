namespace VesselCalc.Domain.Entities
{
    public class MaterialAllowableStress
    {
        public int Id { get; private set; }
        public int MaterialId { get; private set; }
        public decimal Temperature { get; private set; }
        public decimal StressValue { get; private set; }
        public Material Material { get; private set; }

        protected MaterialAllowableStress() { }

        public MaterialAllowableStress(decimal temperature, decimal stressValue)
        {
            Temperature = temperature;
            StressValue = stressValue;
        }
    }
}