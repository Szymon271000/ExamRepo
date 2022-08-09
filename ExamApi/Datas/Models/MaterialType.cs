

namespace Datas.Models
{
    public class MaterialType
    {
        public int MaterialTypeId { get; set; }
        public string? MaterialTypeName { get; set; }

        public string? DefinitionMaterialType { get; set; }

        public List<EducationalMaterial>? educationalMaterials { get; set; }
    }
}
