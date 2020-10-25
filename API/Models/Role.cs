using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Role
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}