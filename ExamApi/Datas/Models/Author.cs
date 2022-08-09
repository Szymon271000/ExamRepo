
namespace Datas.Models
{
    public class Author
    {
        public int AuthorId { get; set; }

        public string? AuthorName { get; set; }
        public string? Description { get; set; }
        public List<EducationalMaterial>? EducationalMaterials { get; set; }

        public int? EducationalMaterialsCounter { get; set; }
    }
}
