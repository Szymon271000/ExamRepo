

namespace Datas.Models
{

    public class User
    {
        public int UserId { get; set; }

        public string? Login { get; set; }

        public List<Role> Roles { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
    }
}
