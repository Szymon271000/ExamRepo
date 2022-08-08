using System.ComponentModel.DataAnnotations;

namespace ExamApi.DTOs.ReviewDTO
{
    public class ReviewToCreateDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string MaterialReviewDescription { get; set; }
        [Required]
        [Range(0,10)]
        public float MaterialReviewDigit { get; set; }
    }
}
