using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Client : IdentityUser
    {

        public string Name { get; set; }

        public string FamilyName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int MembershipTypeId { get; set; }

        public MembershipType MembershipType { get; set; }

        public string Role { get; set; }
    }
}