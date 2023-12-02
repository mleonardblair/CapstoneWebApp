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
        Task<ServiceResponse<List<OrderDetailsResponse>>> GetRecentSalesAsync();
        Task<ServiceResponse<List<OrderDetailsResponse>>> GetMostSuccessfulProductsAsync();
        Task<ServiceResponse<List<OrderDetailsResponse>>> GetTotalSalesAsync();
        Task<ServiceResponse<FinanceSummaryResponse>> GetFinancialSummary();
    }
}
