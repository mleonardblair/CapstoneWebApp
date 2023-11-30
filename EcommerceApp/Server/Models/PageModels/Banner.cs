using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Server.Models.PageModels
{
    public class Banner
    {
        [Key]
        public Guid Id { get; set; }
        public string BannerImageURI { get; set; }
        public DateTime DateModified { get; set; }
    }
}
