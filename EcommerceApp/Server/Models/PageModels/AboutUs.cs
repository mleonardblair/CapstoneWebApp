using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Server.Models.PageModels
{
    public class AboutUs
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Paragraph { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageAlt { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }
    }
}
