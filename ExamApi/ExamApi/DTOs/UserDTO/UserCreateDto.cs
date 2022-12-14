using System.ComponentModel.DataAnnotations;

namespace ExamApi.DTOs.UserDTO
{
    public class UserCreateDTO
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
