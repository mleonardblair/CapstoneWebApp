using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime TimePeriodStart { get; set; }
        public DateTime TimePeriodEnd { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public string Region { get; set; } // New property for region
        public string UserName { get; set; } // New property for user name
    }


}
