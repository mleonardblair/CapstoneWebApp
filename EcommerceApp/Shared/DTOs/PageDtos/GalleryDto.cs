using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs.PageDtos
{
    public class GalleryDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "Gallery";
        public string Subtitle { get; set; } = "This is where I host work and whatnot.";
        public List<string> GalleryImages { get; set; } = new List<string>();
        public DateTime DateModified { get; set; }
    }
}
