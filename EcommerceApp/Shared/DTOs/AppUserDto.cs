using System.ComponentModel.DataAnnotations;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Shared.DTOs
{
    public class AppUserDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "A first name is required."), MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "A last name is required."), MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "A valid email is required."), MaxLength(100), EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        [Required(ErrorMessage = "A valid role is required."), MaxLength(10), EmailAddress]
        public string Role { get; set; } = "Customer"; // default role

    }
}
