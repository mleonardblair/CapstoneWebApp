using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.DTOs.Report;
using EcommerceApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceApp.Server.Services.ReportService
{
    public interface IReportService
    {
        Task<ServiceResponse<List<Order>>> GetRecentSalesAsync();
        Task<ServiceResponse<List<Order>>> GetMostSuccessfulProductsAsync();
        Task<ServiceResponse<List<Order>>> GetTotalSalesAsync();
        Task<ServiceResponse<FinanceSummaryResponse>> GetFinancialSummary();
    }
}
