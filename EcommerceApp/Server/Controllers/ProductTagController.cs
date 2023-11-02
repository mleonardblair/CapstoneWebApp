using EcommerceApp.Server.Services.ProductTagService;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/producttags")]
    [ApiController]
    public class ProductTagController : ControllerBase
    {
        private readonly IProductTagService _productTagService;

        public ProductTagController(IProductTagService productTagService)
        {
            _productTagService = productTagService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ProductTagDto>>> CreateProductTagAsync(ProductTagDto productTag)
        {
            var response = await _productTagService.CreateProductTagAsync(productTag);
            return Ok(response);
        }
    }
}
