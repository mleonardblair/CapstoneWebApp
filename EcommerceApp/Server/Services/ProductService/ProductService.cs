using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Net.Http;
using EcommerceApp.Server.Data;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace EcommerceApp.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        public ProductService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;

            var connectionString = configuration.GetConnectionString("AzureBlobStorage");
            _blobServiceClient = new BlobServiceClient(connectionString);
            _blobContainerName = configuration["BlobStorageContainer"];
        }
        // Upload To Blob Async
        private async Task<string> UploadToBlobAsync(IFormFile file)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = blobContainerClient.GetBlobClient(file.FileName);
            await blobClient.UploadAsync(file.OpenReadStream(), true);
            return blobClient.Uri.ToString();
        }
        private async Task DeleteFromBlobAsync(string blobName)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName);
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<ServiceResponse<ProductDto>> GetProductByIdAsync(Guid productId)
        {
            var response = new ServiceResponse<ProductDto>();
            var product = await _context.Products!
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync(e => e.Id == productId);

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
            var product = _mapper.Map<Product>(productDto);
            var response = new ServiceResponse<ProductDto>();

            // Blob Upload
           /* var blobClient = _blobServiceClient.GetBlobContainerClient(_blobContainerName).GetBlobClient(Guid.NewGuid().ToString());
            await blobClient.UploadAsync(imageStream, new BlobHttpHeaders { ContentType = "image/png" });
            productDto.ImageURI = blobClient.Uri.ToString();*/
            await _context.Products.AddAsync(product);
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
        /*public async Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, IFormFile? imageFile = null)
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
                await _context.Products!.AddAsync(product);
                await _context.SaveChangesAsync();

                *//* // Associate tags
                 foreach (var tagId in productDto.TagIds)
                 {
                     var tag = await _context.Tags!.FindAsync(tagId);
                     if (tag != null)
                     {
                         var productTag = new ProductTag
                         {
                             ProductId = product.Id,
                             TagId = tagId
                         };
                         await _context.ProductTags!.AddAsync(productTag);
                     }
                 }

                 await _context.SaveChangesAsync();*//*

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
        }*/
        public async Task<ServiceResponse<ProductDto>> PostProductAsync(ProductDto productDto, IFormFile? imageFile = null)
        {
            var response = new ServiceResponse<ProductDto>();
            try
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                var product = _mapper.Map<Product>(productDto);
                if (imageFile != null)
                {
                    var imageUrl = await UploadToBlobAsync(imageFile);
                    product.ImageURI = imageUrl;
                    await _context.SaveChangesAsync();
                }
                await _context.Products!.AddAsync(product);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

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
                var products = await _context.Products!
                   .Include(p => p.ProductTags)
                   .ToListAsync();

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
                response.Message = $"An error occurred: {ex.Message}, StackTrace: {ex.StackTrace}, InnerException: {ex.InnerException?.Message}";
            }
            return response;
        }
        public async Task<ServiceResponse<bool>> DeleteProductByIdAsync(Guid productId)
        {
            var response = new ServiceResponse<bool>();
            var product = await _context.Products!.FirstOrDefaultAsync(e => e.Id == productId);

            if (product == null)
            {
                response.Success = false;
                response.Message = "Product not found";
                return response;
            }
            if (!string.IsNullOrEmpty(product.ImageURI))
            {
                var blobName = new Uri(product.ImageURI).Segments.Last();
                await DeleteFromBlobAsync(blobName);
            }
            try
            {
                _context.Products!.Remove(product);
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
                var existingProduct = await _context.Products!.Include(p => p.ProductTags)
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
                existingProduct.ImageURI = productToUpdate.ImageURI;
                existingProduct.StockQuantity = productToUpdate.StockQuantity;

                /*  // Clear existing tags
                  _context.ProductTags!.RemoveRange(existingProduct.ProductTags);

                  // Add new tags
                  foreach (var tagId in productToUpdate.TagIds)
                  {
                      var productTag = new ProductTag { ProductId = existingProduct.Id, TagId = tagId };
                      await _context.ProductTags.AddAsync(productTag);
                  }

                  await _context.SaveChangesAsync();*/

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

        public async Task<ServiceResponse<List<ProductDto>>> GetProductsByCategoryId(Guid categoryId)
        {
            var response = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await _context.Products!
                    .Where(p => p.CategoryId == categoryId)
                    .Include(pt => pt.ProductTags)
                    .ThenInclude(pt => pt.Tag)
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found for the category.";
                }
                else
                {
                    var productDtos = _mapper.Map<List<ProductDto>>(products);
                    response.Data = productDtos;
                    response.Message = "Products retrieved successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}, StackTrace: {ex.StackTrace}, InnerException: {ex.InnerException?.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<ProductDto>>> SearchProducts(string searchQuery)
        {
            var response = new ServiceResponse<List<ProductDto>>()
            {
                Data = await FindProductsBySearchQuery(searchQuery)
            };
            // Need to map to 
          
            return response;
        }

        private async Task<List<ProductDto>> FindProductsBySearchQuery(string searchQuery)
        {
            var products = await _context.Products
                                .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower())
                                            || p.Description.ToLower().Contains(searchQuery.ToLower())
                                            || p.ProductTags.Any(pt => pt.Tag.Name.ToLower().Contains(searchQuery.ToLower())))
                                .ToListAsync();

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }

        /// <summary>
        /// Returns a list of search suggestions based on the search query.
        /// These suggestions are based on the product name and description.
        /// For every word in the description, it will be added as a suggestion.
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchQuery)
        {
            var products = await FindProductsBySearchQuery(searchQuery);

            List<string> result = new List<string>();
            foreach(var product in products)
            {
                if (product.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Name);
                }

                if(product.Description != null)
                {
                    var punctuation = product.Description.Where(Char.IsPunctuation).Distinct().ToArray();
                    var words = product.Description.Split().Select(x => x.Trim(punctuation)); 

                    foreach(var word in words)
                    {
                        if(word.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
                        {
                            result.Add(word);
                        }   
                    }
                }
            }
            return new ServiceResponse<List<string>>
            {
                Data = result
            };
        }
    }

}
