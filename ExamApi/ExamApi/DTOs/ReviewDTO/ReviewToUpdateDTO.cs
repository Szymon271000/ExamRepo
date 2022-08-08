using System.ComponentModel.DataAnnotations;

namespace ExamApi.DTOs.ReviewDTO
{
    public class ReviewToUpdateDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string MaterialReviewDescription { get; set; }
    }
}
