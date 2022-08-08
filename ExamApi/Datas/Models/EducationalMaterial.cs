
using System.ComponentModel.DataAnnotations.Schema;

namespace Datas.Models
{
    public class EducationalMaterial
    {
        public int EducationalMaterialId { get; set; }
        public Author? author { get; set; }
        [ForeignKey("Author")]
        public int? authorId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public MaterialType? materialType { get; set; }
        [ForeignKey("MaterialType")]
        public int? materialTypeId { get; set; }

        public List<MaterialReview>? Reviews = new List<MaterialReview>();

        public DateTime PublishingDate = DateTime.Now;

    }
}
