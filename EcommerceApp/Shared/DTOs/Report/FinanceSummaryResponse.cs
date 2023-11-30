using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Shared.DTOs.Report
{
    public class FinanceSummaryResponse
    {
        public SalesData Sales { get; set; }
        public GrossProfitData GrossProfit { get; set; }
    }
}
