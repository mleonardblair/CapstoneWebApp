using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs.Report
{
    public class GrossProfitData
    {
        public decimal NetSales { get; set; }
        public decimal NetSalesWithoutCostRecorded { get; set; }
        public decimal NetSalesWithCostRecorded { get; set; }
        public decimal CostOfGoodsSold { get; set; }
        public decimal GrossProfits => NetSales - CostOfGoodsSold;
    }
}
