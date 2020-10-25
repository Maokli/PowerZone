using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class ClientLogin
    {
        public string Username { get; set; }

        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public string Token { get; set; }

        public string Role  { get; set; }
    }
}