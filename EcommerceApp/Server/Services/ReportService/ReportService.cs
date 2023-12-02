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
        public async Task<ServiceResponse<List<Report>>> FilterReportsByTimePeriod(DateTime startDate, DateTime endDate)
        {
            var response = new ServiceResponse<List<Report>>();
            try
            {
                response.Data = await _context.Reports
                                              .Where(r => r.TimePeriodStart >= startDate && r.TimePeriodEnd <= endDate)
                                              .ToListAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
/*
        public async Task<ServiceResponse<List<Report>>> FilterReportsByRegion(string region)
        {
            var response = new ServiceResponse<List<Report>>();
            try
            {
                // Assuming the region is a property of the ApplicationUser or related to it
                response.Data = await _context.Reports
                                              .Include(r => r.ApplicationUser)
                                              .Where(r => r.ApplicationUser.Region == region)
                                              .ToListAsync();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }*/
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

        public async Task<ServiceResponse<List<OrderDetailsResponse>>> GetRecentSalesAsync()
        {
            var response = new ServiceResponse<List<OrderDetailsResponse>>();
            try
            {
                var recentSales = await _context.Orders
                    .Where(order => order.Date >= DateTime.Now.AddDays(-7)) // last 7 days
                    .Select(order => new OrderDetailsResponse
                    {
                        OrderDate = order.Date,
                        TotalPrice = order.Total,
                        Products = order.OrderItems.Select(item => new OrderDetailsProductResponse
                        {
                            ProductId = item.ProductId,
                            Name = item.Product.Name,
                            ImageURI = item.Product.ImageURI,
                            Quantity = item.Quantity,
                            TotalPrice = item.Price
                        }).ToList()
                    }).ToListAsync();

                response.Data = recentSales;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message; // Or any other error handling mechanism
            }
            return response;
        }

        public async Task<ServiceResponse<List<OrderDetailsResponse>>> GetMostSuccessfulProductsAsync()
        {
            var response = new ServiceResponse<List<OrderDetailsResponse>>();
            try
            {
                // This query assumes that 'TotalPrice' in 'OrderDetailsProductResponse' represents the total sales of that product
                var successfulProducts = await _context.OrderItems
                    .GroupBy(item => item.ProductId)
                    .OrderByDescending(g => g.Sum(item => item.Quantity*item.Price))
                    .Select(g => new OrderDetailsResponse
                    {
                        // Additional details can be added as per requirement
                        Products = g.Select(item => new OrderDetailsProductResponse
                        {
                            ProductId = item.ProductId,
                            Name = item.Product.Name,
                            ImageURI = item.Product.ImageURI,
                            Quantity = item.Quantity,
                            TotalPrice = item.Price
                        }).ToList()
                    }).ToListAsync();

                response.Data = successfulProducts;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<OrderDetailsResponse>>> GetTotalSalesAsync()
        {
            var response = new ServiceResponse<List<OrderDetailsResponse>>();
            try
            {
                var totalSales = await _context.Orders
                    .Select(order => new OrderDetailsResponse
                    {
                        OrderDate = order.Date,
                        TotalPrice = order.Total,
                        Products = order.OrderItems.Select(item => new OrderDetailsProductResponse
                        {
                            ProductId = item.ProductId,
                            Name = item.Product.Name,
                            ImageURI = item.Product.ImageURI,
                            Quantity = item.Quantity,
                            TotalPrice = item.Price
                        }).ToList()
                    }).ToListAsync();

                response.Data = totalSales;
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

    }
}
