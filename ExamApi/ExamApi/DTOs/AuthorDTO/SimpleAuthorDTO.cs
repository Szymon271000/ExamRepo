using ExamApi.DTOs.EducationalMaterialDTO;

namespace ExamApi.DTOs.AuthorDTO
{
    public class SimpleAuthorDTO
    {
        public string AuthorName { get; set; }
        public string Description { get; set; }

        public IEnumerable<SimpleEducationalMaterial> simpleEducationalMaterials { get; set; }
        public int? EducationalMaterialsCounter { get; set; }

    }
}
