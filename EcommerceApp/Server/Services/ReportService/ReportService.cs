using EcommerceApp.Server.Data;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.DTOs.Report;
using EcommerceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Order>>> GetRecentSalesAsync()
        {
            var data = await _context.Orders
                                     .Where(o => o.Date >= DateTime.Now.AddDays(-7))
                                     .ToListAsync();
            return new ServiceResponse<List<Order>> { Data = data };
        }

        public async Task<ServiceResponse<List<Order>>> GetMostSuccessfulProductsAsync()
        {
            var data = await _context.Orders
                                     .Include(o => o.OrderItems)
                                     .GroupBy(o => o.OrderItems.Select(oi => oi.ProductId))
                                     .OrderByDescending(g => g.Sum(oi => oi.OrderItems.Sum(i => i.Quantity)))
                                     .SelectMany(g => g)
                                     .ToListAsync();
            return new ServiceResponse<List<Order>> { Data = data };
        }

        public async Task<ServiceResponse<List<Order>>> GetTotalSalesAsync()
        {
            var data = await _context.Orders.ToListAsync();
            return new ServiceResponse<List<Order>> { Data = data };
        }

        public async Task<ServiceResponse<FinanceSummaryResponse>> GetFinancialSummary()
        {
            // Calculate each part of the summary
            var salesData = await CalculateSalesData();
            var grossProfitData = await CalculateGrossProfitData(salesData);

            // Combine the parts into the FinanceSummaryResponse
            var summary = new FinanceSummaryResponse
            {
                Sales = salesData,
                GrossProfit = grossProfitData
            };

            return new ServiceResponse<FinanceSummaryResponse> { Data = summary };
        }

        /// <summary>
        /// When called will calculate the sales data for the FinanceSummaryResponse.
        /// </summary>
        /// <returns></returns>
        private async Task<SalesData> CalculateSalesData()
        {
            var orders = await _context.Orders.ToListAsync();
            var grossSales = orders.Sum(o => o.Subtotal);
            var discounts = orders.Sum(o => o.Discount);
            var returns = orders.Where(o => o.Status == OrderStatus.Returned).Sum(o => o.Subtotal);
/*            var shipping = orders.Sum(o => o.Shipping); // Assuming Shipping is a property of Order*/
            var taxes = orders.Sum(o => o.Tax);

            return new SalesData
            {
                GrossSales = grossSales,
                Discounts = discounts,
                Returns = returns,
/*                Shipping = shipping,*/
                Taxes = taxes
            };
        }

        /// <summary>
        /// When called will calculate the gross profit data for the FinanceSummaryResponse.
        /// </summary>
        /// <param name="salesData"></param>
        /// <returns></returns>
        private async Task<GrossProfitData> CalculateGrossProfitData(SalesData salesData)
        {
            var ordersWithCost = await _context.Orders
                                               .Include(o => o.OrderItems)
                                               .ToListAsync();

            var netSalesWithCostRecorded = ordersWithCost.Sum(o => o.Subtotal);
            var discounts = ordersWithCost.Sum(o => o.Discount);
            var cogs = ordersWithCost
                .Sum(o => o.OrderItems
                .Sum(oi => oi.Price * oi.Quantity));
            // Basically returns 
            var netSalesWithoutCostRecorded = 
                salesData.GrossSales - netSalesWithCostRecorded; // grossSales from CalculateSalesData method

            return new GrossProfitData
            {
                NetSales = salesData.GrossSales - salesData.Discounts - salesData.Returns, // Values from CalculateSalesData method
                NetSalesWithoutCostRecorded = netSalesWithoutCostRecorded,
                NetSalesWithCostRecorded = netSalesWithCostRecorded,
                CostOfGoodsSold = cogs
            };
        }
    }
}
