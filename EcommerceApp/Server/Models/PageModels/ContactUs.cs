using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Server.Models.PageModels
{
    public class ContactUs
    {
        [Key]
        public Guid Id { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public string BusinessAddress { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }
    }
}
