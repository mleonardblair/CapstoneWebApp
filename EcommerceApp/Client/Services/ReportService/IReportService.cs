using EcommerceApp.Server.Services.ReportService;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.DTOs.Report;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace EcommerceApp.Client.Services.ReportService
{
    public interface IReportService
    {
        Task<ServiceResponse<List<OrderDetailsResponse>>>GetTotalSalesAsync();
        Task<ServiceResponse<List<OrderDetailsResponse>>> GetMostSuccessfulProductsAsync();
        Task<ServiceResponse<List<OrderDetailsResponse>>> GetRecentSalesAsync();
        /*        Task<ServiceResponse<List<FinanceSummaryResponse>>> GetRecentSalesAsync();
                        Task<ServiceResponse<List<FinanceSummaryResponse>>> GetMostSuccessfulProductsAsync();
                        Task<ServiceResponse<List<FinanceSummaryResponse>>> GetTotalSalesAsync();*/
        Task<ServiceResponse<FinanceSummaryResponse>> GetFinancialSummary();
        public event Action ReportChanged;
        public string Message { get; set; }
        public FinanceSummaryResponse FinanceSummary { get; set; }
        Task<ServiceResponse<List<ReportDto>>> FilterReportsByTimePeriod(DateTime startDate, DateTime endDate);
        Task<ServiceResponse<List<ReportDto>>> FilterReportsByRegion(string region);
    }
}
