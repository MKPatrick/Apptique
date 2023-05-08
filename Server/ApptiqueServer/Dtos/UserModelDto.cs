using System.ComponentModel.DataAnnotations;

namespace ApptiqueServer.Dtos
{
    public class UserModelDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
