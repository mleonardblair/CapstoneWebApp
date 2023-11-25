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
        public string AddressLine { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string City { get; set; } = string.Empty;

        [Required, MaxLength(50)] public string Province { get; set; } = string.Empty;    

        [Required, MaxLength(50)] public string Country { get; set; } = string.Empty;

        [MaxLength(50)]
        public string PostalCode { get; set; } = string.Empty;
        public DateTime DateModified {  get; set; }

        public ICollection<UserAddress> UserAddresses { get; set; } = new List<UserAddress>();
    }
 
}
