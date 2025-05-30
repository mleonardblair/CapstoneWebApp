﻿using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.DTOs.Report;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.ReportService
{

    public class ReportService : IReportService
    {
        public event Action ReportChanged;
        public FinanceSummaryResponse FinanceSummary { get; set; } = new FinanceSummaryResponse();
        public string Message { get; set; }

        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        public ReportService(HttpClient httpClient,
          AuthenticationStateProvider authStateProvider,
          NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        public async Task<ServiceResponse<List<ReportDto>>> FilterReportsByTimePeriod(DateTime startDate, DateTime endDate)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ReportDto>>>($"api/reports/filterByTimePeriod?startDate={startDate}&endDate={endDate}");
            return response;
        }

        public async Task<ServiceResponse<List<ReportDto>>> FilterReportsByRegion(string region)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<ReportDto>>>($"api/reports/filterByRegion?region={region}");
            return response;
        }
        public async Task<ServiceResponse<FinanceSummaryResponse>> GetFinancialSummary()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<FinanceSummaryResponse>>
                ($"api/report/admin");
            if(response != null)
            {
                // update the FinanceSummary property
                FinanceSummary = response.Data;
            }
            return new ServiceResponse<FinanceSummaryResponse>
            {
                Data = null,
                Success = true
            };
        }

        public async Task<ServiceResponse<List<OrderDetailsResponse>>> GetRecentSalesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderDetailsResponse>>>("api/reports/GetRecentSales");
            return response;
        }

        public async Task<ServiceResponse<List<OrderDetailsResponse>>> GetMostSuccessfulProductsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderDetailsResponse>>>("api/reports/GetMostSuccessfulProducts");
            return response;
        }

        public async Task<ServiceResponse<List<OrderDetailsResponse>>> GetTotalSalesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<OrderDetailsResponse>>>("api/reports/GetTotalSales");
            return response;
        }
    }
}
