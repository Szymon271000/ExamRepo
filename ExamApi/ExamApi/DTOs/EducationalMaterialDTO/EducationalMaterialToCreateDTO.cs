using System.ComponentModel.DataAnnotations;

namespace ExamApi.DTOs.EducationalMaterialDTO
{
    public class EducationalMaterialToCreateDTO
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Description { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Location { get; set; }
    }
}
