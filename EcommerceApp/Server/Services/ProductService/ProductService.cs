using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Security.Claims;
using System.Net.Http;
using EcommerceApp.Server.Data;

namespace EcommerceApp.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId)
        {
            var response = new ServiceResponse<ProductDto>();
            var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == productId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Product not found.";
            }
            else
            {
                response.Data = _mapper.Map<ProductDto>(product);
            }
            return response;
        }
        public async Task<ServiceResponse<ProductDto>> CreateProductAsync(ProductDto productDto)
        {

            // Map the product to a DTO
            var product = _mapper.Map<Product>(productDto); // Note: You should map to Product, not ProductDto
            var response = new ServiceResponse<ProductDto>();

            await _context.AddAsync(product);
            // Save the product
            var result = await _context.SaveChangesAsync();
            if (result <= 0)
            {
                response.Success = false;
                response.Message = "Failed to add product.";
            }
            else
            {
                // if successful Map the Product back to a ProductDTO
                var productDtoOut = _mapper.Map<ProductDto>(product);
                response.Data = productDtoOut;
                response.Message = "Product successfully added.";
            }
            return response;
        }
        public async Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, IFormFile? imageFile = null)
        {
            var response = new ServiceResponse<ProductDto>();

            try
            {
                // Handle image upload
                if (imageFile != null)
                {
                    using var memoryStream = new MemoryStream();
                    await imageFile.CopyToAsync(memoryStream);
                    productDto.Image = memoryStream.ToArray();
                }

                // Begin db transaction, to ensure that either the multiple changes are applied and work or none do for data integrity..
                using var transaction = await _context.Database.BeginTransactionAsync();

                // Create product
                var product = _mapper.Map<Product>(productDto);
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();

                // Associate tags
                foreach (var tagId in productDto.TagIds)
                {
                    var tag = await _context.Tags.FindAsync(tagId);
                    if (tag != null)
                    {
                        var productTag = new ProductTag
                        {
                            ProductId = product.Id,
                            TagId = tagId
                        };
                        await _context.ProductTags.AddAsync(productTag);
                    }
                }

                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                // Map the Product back to a ProductDTO
                var productDtoOut = _mapper.Map<ProductDto>(product);
                response.Data = productDtoOut;
                response.Message = "Product successfully added.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Message = $"An error occurred: {ex.Message}";
                response.Success = false;
            }

            return response;
        }
        public async Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await _context.Products.ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found.";
                }
                else
                {
                    response.Data = _mapper.Map<List<ProductDto>>(products);
                    response.Message = "Products retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId)
        {
            var response = new ServiceResponse<bool>();
            var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == productId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Product not found";
                return response;
            }

            try
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                response.Data = true;
                response.Message = "Product deleted successfully";
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = $"An error occurred: {e.Message}";
            }

            return response;
        }
        public async Task<ServiceResponse<ProductDto>> UpdateProductByIdAsync(Guid productId, [FromBody] ProductDto productToUpdate)
        {
            var response = new ServiceResponse<ProductDto>();
            try
            {
                var existingProduct = await _context.Products.Include(p => p.ProductTags)
                                                             .FirstOrDefaultAsync(p => p.Id == productId);
                if (existingProduct == null)
                {
                    response.Success = false;
                    response.Message = "No product found to update.";
                    return response;
                }

                // Update product properties
                existingProduct.Name = productToUpdate.Name;
                existingProduct.Price = productToUpdate.Price;
                existingProduct.Description = productToUpdate.Description;
                existingProduct.CategoryId = productToUpdate.CategoryId;
                existingProduct.Image = productToUpdate.Image;
                existingProduct.StockQuantity = productToUpdate.StockQuantity;

                // Clear existing tags
                _context.ProductTags.RemoveRange(existingProduct.ProductTags);

                // Add new tags
                foreach (var tagId in productToUpdate.TagIds)
                {
                    var productTag = new ProductTag { ProductId = existingProduct.Id, TagId = tagId };
                    await _context.ProductTags.AddAsync(productTag);
                }

                await _context.SaveChangesAsync();

                // Map the Product back to a ProductDTO
                var productDtoOut = _mapper.Map<ProductDto>(existingProduct);
                response.Data = productDtoOut;
                response.Message = "Product updated successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }
            return response;
        }

    }
}
