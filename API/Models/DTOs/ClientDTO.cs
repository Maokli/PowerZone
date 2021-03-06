using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs
{
    public class ClientDTO
    {
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string FamilyName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int MembershipTypeId { get; set; }

        public MembershipType MembershipType { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        

        

    }
}