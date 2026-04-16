namespace VesselCalc.Domain.Entities
{
    public class Material
    {
        public int Id { get; private set; }
        public string Specification { get; private set; }
        public string Grade { get; private set; }
        public string ProductForm { get; private set; }
        public decimal MinTensileStrength { get; private set; }
        public decimal MinYieldStrength { get; private set; }

        private readonly List<MaterialAllowableStress> _allowableStresses = new();
        public IReadOnlyCollection<MaterialAllowableStress> AllowableStresses => _allowableStresses.AsReadOnly();

        // Construtor vazio exigido pelo EF Core
        protected Material() { }

        public Material(string specification, string grade, string productForm, decimal minTensile, decimal minYield)
        {
            Specification = specification;
            Grade = grade;
            ProductForm = productForm;
            MinTensileStrength = minTensile;
            MinYieldStrength = minYield;
        }

        // Método para encapsular a regra de adicionar tensões
        public void AddAllowableStress(decimal temperature, decimal stressValue)
        {
            var stress = new MaterialAllowableStress(temperature, stressValue);
            _allowableStresses.Add(stress);
        }
    }
}