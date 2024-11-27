using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Book_Management.Models
{
    public class User : IdentityUser
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordHash { get; set; } = string.Empty; // Store hashed password

        public string Role { get; set; } = string.Empty;// Optionally, store the user's role
    }
}