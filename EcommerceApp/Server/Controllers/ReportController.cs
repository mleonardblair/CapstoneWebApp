using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.DTOs.Report;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        // injection of IReportService
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // GET: api/<ReportController>
        [HttpGet("admin")]
        public async Task<ActionResult<ServiceResponse<FinanceSummaryResponse>>> GetFinancialSummary()
        {
            var response = await _reportService.GetFinancialSummary();
            return Ok(response);
        }

     
/*
        [HttpGet("filterByRegion")]
        public async Task<ActionResult<ServiceResponse<List<ReportDto>>>> FilterReportsByRegion(string region)
        {
            var response = await _reportService.FilterReportsByRegion(region);
            return Ok(response);
        }*/
    }
}
