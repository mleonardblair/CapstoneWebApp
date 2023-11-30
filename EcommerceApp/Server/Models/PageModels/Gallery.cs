using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Server.Models.PageModels
{
    public class Gallery
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = "Gallery";
        public string Subtitle { get; set; } = "This is where I host work and whatnot.";
        public virtual ICollection<GalleryImage> GalleryImages { get; set; } = new List<GalleryImage>();
        public DateTime DateModified { get; set; }


    }
    public class GalleryImage
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageURI { get; set; } = string.Empty;
        public Guid GalleryId { get; set; }
        public virtual Gallery? Gallery { get; set; }
    }
}
