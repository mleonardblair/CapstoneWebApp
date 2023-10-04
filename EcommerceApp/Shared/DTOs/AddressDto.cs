using System;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Shared.DTOs
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public AddressType AddressType { get; set; }
    }
}
