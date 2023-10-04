using System.ComponentModel.DataAnnotations;
using EcommerceApp.Shared;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(255)]
        public string? AddressLine { get; set; }

        [Required, MaxLength(100)]
        public string? City { get; set; }

        [Required, MaxLength(50)]
        public string? Province { get; set; }

        [Required, MaxLength(50)]
        public string? Country { get; set; }

        [MaxLength(50)]
        public string? PostalCode { get; set; }
        [Required]
        public DateTime DateCreated {  get; set; }= DateTime.UtcNow;
        public DateTime DateModified {  get; set; }
        public AddressType AddressType {  get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
    }
 
}
