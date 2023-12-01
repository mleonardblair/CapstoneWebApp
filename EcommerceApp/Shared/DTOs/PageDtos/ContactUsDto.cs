using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs.PageDtos
{
    public class ContactUsDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Telephone { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public string BusinessAddress { get; set; } = string.Empty;
        public DateTime DateModified { get; set; }
    }
}
