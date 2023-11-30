using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs.Report
{
    public class SalesData
    {
        public decimal GrossSales { get; set; }
        public decimal Discounts { get; set; }
        public decimal Returns { get; set; }
        public decimal NetSales => GrossSales - Discounts - Returns;
        public decimal Shipping { get; set; }
        public decimal Taxes { get; set; }
        public decimal TotalSales => NetSales + Shipping + Taxes;
    }
}
