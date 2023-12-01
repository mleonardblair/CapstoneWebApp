using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs.PageDtos
{
    public class AboutUsDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Paragraph { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string ImageAlt { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }
    }
}
