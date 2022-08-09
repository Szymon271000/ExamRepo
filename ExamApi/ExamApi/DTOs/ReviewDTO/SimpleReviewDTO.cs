using ExamApi.DTOs.EducationalMaterialDTO;

namespace ExamApi.DTOs.ReviewDTO
{
    public class SimpleReviewDTO
    {
        public int MaterialReviewId { get; set; }

        public string educationalMaterial { get; set; }

        public string MaterialReviewDescription { get; set; }
        public float MaterialReviewDigit { get; set; }
    }
}
