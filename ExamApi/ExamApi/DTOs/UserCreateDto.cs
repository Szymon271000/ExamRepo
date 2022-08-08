using System.ComponentModel.DataAnnotations;

namespace ExamApi.DTOs
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Login { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string Password { get; set; }
    }
}
