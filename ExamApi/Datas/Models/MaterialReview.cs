
using System.ComponentModel.DataAnnotations.Schema;

namespace Datas.Models
{
    public class MaterialReview
    {
        public int MaterialReviewId { get; set; }

        public EducationalMaterial? educationalMaterial { get; set; }
        [ForeignKey("EducationalMaterial")]
        public int? educationalMaterialId { get; set; }
        public string? MaterialReviewDescription { get; set; }
        public float? MaterialReviewDigit { get; set; }
    }
}
