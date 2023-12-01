using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs.PageDtos
{
    public class FAQDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public List<string> Questions { get; set; } = new List<string>();
        public List<string> Answers { get; set; } = new List<string>();
        public DateTime DateModified { get; set; }
    }
   
}
