using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using EcommerceApp.Server.Services.ProductService;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Asynchronous call to get a list of products, basically IActionResult defines a contract that is the result of a request.
        // When the client (in my case, the Blazor WASM app) sends a GET request to the server to ask for products, this method will be executed.The IActionResult is a way for the server to package up its response to the client's HTTP request. and The Ok(Products) part essentially means that the server is responding with a "200 OK" status code along with the data (Products in this example). This is a standard way to say "Hey, everything went well, and here's the data you asked for."
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllProductsAsync()
        {
            var response = await _productService.GetAllProductsAsync();
            return Ok(response);
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByIdAsync(Guid productId)
        {
            var response = await _productService.GetProductByIdAsync(productId);
            return Ok(response);
        }
      
        [HttpPost("Create")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> CreateProductAsync(ProductDto productDto)
        {
            var response = await _productService.CreateProductAsync(productDto);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> PostProductAsync([FromForm] ProductDto productDto, [FromForm] IFormFile imageFile)
        {

            // Read the request body
            HttpContext.Request.EnableBuffering(); // Enable buffering so you can read the stream multiple times
            string requestBody;
            using (StreamReader reader = new StreamReader(HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            HttpContext.Request.Body.Position = 0; // Reset the stream position for model binding

            // Log the request body
            Console.WriteLine($"Raw Request Body: {requestBody}");
            var response = await _productService.PostProductAsync(productDto, imageFile);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }


        
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProductByIdAsync(Guid productId)
        {
            var response = await _productService.DeleteProductByIdAsync(productId);
            if (response.Success)
                return Ok(response);
            return NotFound(response);
        }



        [HttpPut("{productId}")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> UpdateProductByIdAsync(Guid productId, [FromBody] ProductDto product)
        {
            var response = await _productService.UpdateProductByIdAsync(productId, product);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response); // or NotFound(response) based on your logic
        }

    }
}
