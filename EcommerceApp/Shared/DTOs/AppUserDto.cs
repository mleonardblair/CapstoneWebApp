using System.ComponentModel.DataAnnotations;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Shared.DTOs
{
    public class AppUserDto
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public string Role { get; set; } = "Customer"; // default role

    }
}
