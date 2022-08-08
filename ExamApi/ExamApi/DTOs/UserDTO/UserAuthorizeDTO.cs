using System.ComponentModel.DataAnnotations;

namespace ExamApi.DTOs.UserDTO
{
    public class UserAuthorizeDTO
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
