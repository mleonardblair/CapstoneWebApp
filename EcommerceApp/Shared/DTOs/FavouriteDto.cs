using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs
{
    public class FavouriteDto
    {
        public Guid Id { get; set; }
        public string ApplicationUserId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }

}
