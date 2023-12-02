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

        /// <summary>
        /// Overloaded route for pagination.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> AddOrUpdate(ProductDto product)
        {
            return Ok(await _productService.AddOrUpdateProductAsync(product));
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategoryId(Guid categoryId)
        {
            var response = await _productService.GetProductsByCategoryId(categoryId);
            return Ok(response);
        }


        [HttpGet("{productId}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductByIdAsync(Guid productId)
        {
            var response = await _productService.GetProductByIdAsync(productId);
            return Ok(response);
        }

        [HttpGet("suggestions/{searchQuery}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetProductSearchSuggestions(string searchQuery)
        {
            var response = await _productService.GetProductSearchSuggestions(searchQuery);
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

        /*[HttpPost]
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
        }*/

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

        [HttpGet("search")]
        public async Task<ActionResult<ServiceResponse<ProductPaginationResponse>>> SearchProducts(
        [FromQuery] string searchQuery,
        [FromQuery] int? page,
        [FromQuery] bool? isAscending,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
        {
            if (string.IsNullOrEmpty(searchQuery) || !page.HasValue)
            {
                return Ok(
                    new ServiceResponse<ProductPaginationResponse>
                    {
                        Data = new ProductPaginationResponse
                        {
                            Products = new List<ProductDto>(),
                            CurrentPage = 0,
                            Pages = 0

                        },
                        Message = "A search query is required.",
                        StatusCode = 404,
                        Success = false
                    }
                );
            }
            // Call the appropriate ProductService method based on the provided query parameters
            if (isAscending.HasValue && minPrice.HasValue && maxPrice.HasValue)
            {
                return Ok(await _productService.SearchProducts(searchQuery, page.Value, isAscending.Value, minPrice.Value, maxPrice.Value));
            }
            else if (minPrice.HasValue && maxPrice.HasValue)
            {
                return Ok(await _productService.SearchProducts(searchQuery, page.Value, minPrice.Value, maxPrice.Value));
            }
            else if (isAscending.HasValue)
            {
                return Ok(await _productService.SearchProducts(searchQuery, page.Value, isAscending.Value));
            }
            else
            {
                return Ok(await _productService.SearchProducts(searchQuery, page.Value));
            }
        }

        [HttpGet("tag")]
        public async Task<ActionResult<ServiceResponse<ProductPaginationResponse>>> GetProductsByTag(
        [FromQuery] Guid? tagId,
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] bool? isAscending,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice)
        {
            if (!tagId.HasValue || !page.HasValue || !pageSize.HasValue)
            {
                return Ok(
                    new ServiceResponse<ProductPaginationResponse>
                    {
                        Data = new ProductPaginationResponse
                        {
                            Products = new List<ProductDto>(),
                            CurrentPage = 0,
                            Pages = 0

                        },
                        Message = "Something went wrong! Try back again later.",
                        StatusCode = 404,
                        Success = false
                    }
                );
            }

            if (isAscending.HasValue && minPrice.HasValue && maxPrice.HasValue)
            {
                return Ok(await _productService.GetProductsByTagId(page.Value, pageSize.Value, isAscending.Value, tagId.Value, minPrice.Value, maxPrice.Value));
            }
            else if (minPrice.HasValue && maxPrice.HasValue)
            {
                return Ok(await _productService.GetProductsByTagId(page.Value, pageSize.Value, tagId.Value, minPrice.Value, maxPrice.Value));
            }
            else if (isAscending.HasValue)
            {
                return Ok(await _productService.GetProductsByTagId(page.Value, pageSize.Value, isAscending.Value, tagId.Value));
            }
            else
            {
                return Ok(await _productService.GetProductsByTagId(page.Value, pageSize.Value, tagId.Value));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<ProductPaginationResponse>>> GetAllProductsAsync(
        [FromQuery] int? page,
        [FromQuery] int? pageSize,
        [FromQuery] bool? isAscending,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] Guid? categoryId)
        {
            if (page.HasValue && pageSize.HasValue && isAscending.HasValue && minPrice.HasValue && maxPrice.HasValue && categoryId.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, isAscending.Value, minPrice.Value, maxPrice.Value, categoryId.Value));
            }
            else if (page.HasValue && pageSize.HasValue && isAscending.HasValue && minPrice.HasValue && maxPrice.HasValue && categoryId.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, isAscending.Value, minPrice.Value, maxPrice.Value, categoryId.Value));
            }
            else if (page.HasValue && pageSize.HasValue && minPrice.HasValue && maxPrice.HasValue && categoryId.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, minPrice.Value, maxPrice.Value, categoryId.Value));
            }
            else if (page.HasValue && pageSize.HasValue && isAscending.HasValue && categoryId.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, isAscending.Value, categoryId.Value));
            }
            else if (page.HasValue && pageSize.HasValue && minPrice.HasValue && maxPrice.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, minPrice.Value, maxPrice.Value));
            }
            else if (page.HasValue && pageSize.HasValue && minPrice.HasValue) 
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, minPrice.Value));
            }
            else if (page.HasValue && pageSize.HasValue && maxPrice.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, maxPrice.Value));
            }
            else if (page.HasValue && pageSize.HasValue && isAscending.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, isAscending.Value));

            }
            else if (page.HasValue && pageSize.HasValue && categoryId.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value, categoryId.Value));

            }
            else if (categoryId.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(categoryId.Value));
            }
            else if (page.HasValue && pageSize.HasValue)
            {
                return Ok(await _productService.GetAllProductsAsync(page.Value, pageSize.Value));
            }
            else
            {
                return Ok(await _productService.GetAllProductsAsync());
            }
        }
        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetAdminProducts()
        {
            var result = await _productService.GetAdminProducts();
            return Ok(result);
        }
        // Get product by id 
        [HttpGet("admin/{productId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<ProductDto>>> GetAdminProductById(Guid productId)
        {
            var result = await _productService.GetAdminProductById(productId);
            return Ok(result);
        }

        

        [HttpDelete("admin/{productId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> DeleteProduct(Guid productId)
        {
            var result = await _productService.DeleteProduct(productId);
            return Ok(result);
        }

        [HttpPost("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddProduct(ProductDto product)
        {
            var result = await _productService.AddProduct(product);
            return Ok(result);
        }

        [HttpPut("admin/{productId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateProduct(ProductDto product)
        {
            var result = await _productService.UpdateProduct(product);
            return Ok(result);
        }
    }
}

