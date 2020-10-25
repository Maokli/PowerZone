using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs
{
    public class MembershipTypeDTO
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public double Price { get; set; }
    }
}