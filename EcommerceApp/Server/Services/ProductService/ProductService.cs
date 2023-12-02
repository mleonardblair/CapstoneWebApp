using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Azure.Storage.Blobs;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace EcommerceApp.Server.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITagService _tagService;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string _blobContainerName;

        public ProductService(AppDbContext context, IMapper mapper, IConfiguration configuration, ITagService TagService)
        {
            _context = context;
            _mapper = mapper;
            _tagService = TagService;

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
                 .Where(p => !p.Deleted)
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
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
              

                // Map the product DTO to a Product entity
                var product = _mapper.Map<Product>(productDto);

                // Add the product to the context
                await _context.Products.AddAsync(product);

                // Save changes to insert the product
                await _context.SaveChangesAsync();

                if (productDto.TagNames != null && productDto.TagNames.Count > 0)
                {
                    // Iterate over the tag names and get or create tags
                    foreach (var tagName in productDto.TagNames)
                    {
                        // Communicates inter-service on server, so doesn't return a DTO.
                        var tag = await _tagService.GetOrCreateTagAsync(tagName);

                        // Create and add a new ProductTag association
                        var productTag = new ProductTag { ProductId = product.Id, TagId = tag.Id };
                        await _context.ProductTags.AddAsync(productTag);
                    }

                    // Save changes for tags and product-tags associations
                    await _context.SaveChangesAsync();
                }

                // Commit the transaction
                await transaction.CommitAsync();

                // Map back to DTO and return success response
                var productDtoOut = _mapper.Map<ProductDto>(product);
                return new ServiceResponse<ProductDto>
                {
                    Data = productDtoOut,
                    Message = "Product successfully added."
                };
            }
            catch (Exception)
            {
                // Rollback the transaction if any exception occurs
                await transaction.RollbackAsync();
                return new ServiceResponse<ProductDto>
                {
                    Success = false,
                    Message = "Failed to add product."
                };
            }
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
        /// <summary>
        /// Gets a list of all products filtered by category id.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync(Guid categoryId)
        {
            var response = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await _context.Products!
                     .Where(p => !p.Deleted) 
                   .Include(p => p.ProductTags)
                   .Where(p=>p.CategoryId == categoryId)
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
        public async Task<ServiceResponse<List<ProductDto>>> GetAllProductsAsync()
        {
            var response = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await _context.Products!.Where(p => !p.Deleted)
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

        /// <summary>
        /// Overload for getallproducts that will return a paginated list of products.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();
            try
            {
                int totalProducts = await _context.Products.CountAsync();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                var products = await _context.Products.Where(p => !p.Deleted)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Include(p => p.ProductTags)
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found.";
                }
                else
                {
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = page,
                        Pages = totalPages
                    };
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

        public async Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, bool isAscending)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();
            try
            {
                int totalProducts = await _context.Products.Where(p => !p.Deleted).CountAsync();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
                // Sorting products based on the product name in ascending or descending order
                var productsQuery = _context.Products.Where(p => !p.Deleted).AsQueryable();


                productsQuery = isAscending ? productsQuery.OrderBy(p => p.Name) : productsQuery.OrderByDescending(p => p.Name);

                var products = await productsQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Include(p => p.ProductTags)
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found.";
                }
                else
                {
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = page,
                        Pages = totalPages
                    };
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

        /// <summary>
        /// Overload for getallproducts that will return a paginated list of products + category filtering.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, Guid categoryId)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();
            try
            {

                // Filter the products by category first
                var query = _context.Products.Where(p => !p.Deleted).AsQueryable();
                if (categoryId != Guid.Empty)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                // Now get the count of products after filtering
                int totalProducts = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Then get the actual page of products
                var products = await query
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .Include(p => p.ProductTags)
                   .ToListAsync();



                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found.";

                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = 1,
                        Pages = 0
                    };
                }
                else
                {
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = page,
                        Pages = totalPages
                    };
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
        public async Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, bool isAscending, Guid categoryId)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();
            try
            {
                // Filter the products by category first
                var query = _context.Products.Where(p => !p.Deleted).AsQueryable();
                if (categoryId != Guid.Empty)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                // Apply sorting based on the product name
                query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);

                // Now get the count of products after filtering
                int totalProducts = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Then get the actual page of products
                var products = await query
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .Include(p => p.ProductTags)
                   .ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found.";
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = 1,
                        Pages = 0
                    };
                }
                else
                {
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = page,
                        Pages = totalPages
                    };
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


        /// <summary>
        /// When called gets all tags.
        /// </summary>
        /// <param name="page">The page size</param>
        /// <param name="pageSize"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, bool isAscending, Guid tagId)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();
            // Filter the products by category first
            var query = _context.Products.Where(p => !p.Deleted).AsQueryable();
            if (tagId != Guid.Empty)
            {
                // Query products by tag
                query = _context.Products
                .Where(p => p.ProductTags.Any(pt => pt.TagId == tagId));
            }

            // Apply sorting based on the product name
            query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
            // Now get the count of products after filtering
            int totalProducts = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);
            // Then get the actual page of products
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "No products found for the tag.";
            }
            else
            {
                response.Data = new ProductPaginationResponse
                {
                    Products = _mapper.Map<List<ProductDto>>(products),
                    CurrentPage = page,
                    Pages = totalPages
                };
                response.Message = "Products retrieved successfully.";
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
                if (product.Deleted)
                {
                    response.Success = false;
                    response.Message = "Product already deleted";
                    return response;
                }else
                {
                    _context.Products!.Remove(product);
                    await _context.SaveChangesAsync();
                    response.Data = true;
                    response.Message = "Product deleted successfully";
                }

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
                var existingProduct = await _context.Products!.Where(p => !p.Deleted)!.Include(p => p.ProductTags)
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
        /// <summary>
        /// Retrieves a list of products associated with a specific category ID.
        /// </summary>
        /// <remarks>
        /// This method performs the following:
        /// - Queries the products that are linked to the specified category ID.
        /// - Includes associated product tags in the query.
        /// - Converts the retrieved products to a list of <see cref="ProductDto"/>.
        /// If no products are found for the given category ID, the method returns a response indicating the absence of products.
        /// In case of an exception, the method captures the error details in the response.
        /// </remarks>
        /// <param name="categoryId">The unique identifier of the category used to filter products.</param>
        /// <returns>
        /// A <see cref="ServiceResponse{T}"/> containing a List of <see cref="ProductDto"/> objects.
        /// The response includes the list of products and a success or error message.
        /// </returns>
        /// <exception cref="Exception">Thrown when an error occurs during the database query process.</exception>

        public async Task<ServiceResponse<List<ProductDto>>> GetProductsByCategoryId(Guid categoryId)
        {
            var response = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await _context.Products!
                    .Where(p => !p.Deleted)
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


        /// <summary>
        /// Performs a paginated search for products based on a given search query.
        /// </summary>
        /// <remarks>
        /// The method encompasses:
        /// - Searching for products where the product name, description, or associated tag names match the search query.
        /// - Implementing pagination to manage the volume of search results.
        /// - Converting the search results to <see cref="ProductDto"/> for consistent data handling.
        /// The search is conducted in a case-insensitive manner, and the method returns a paginated list of products that match the query.
        /// </remarks>
        /// <param name="searchQuery">The search string used to find matching products.</param>
        /// <param name="page">The page number to retrieve in the paginated search result.</param>
        /// <returns>
        /// A <see cref="ServiceResponse{T}"/> containing a <see cref="ProductPaginationResponse"/> object.
        /// The response includes the paginated list of products, the total number of pages, the current page, and a success message.
        /// </returns>
        public async Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page)
        {
            var pageResults = 2f;
            var numberOfPages = Math.Ceiling((await FindProductsBySearchQuery(searchQuery))
                .Count / pageResults);
            // This is the query that will be used to find the products that match the search query
            // and will be used to find the
            // al number of pages for the search results pagination
            // and will be used to find the products for the requested page number
            var products = await _context.Products
                .Where(p => !p.Deleted)
                                .Include(p => p.ProductTags)
                                .ThenInclude(pt => pt.Tag)
                                .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower())
                                 || p.Description.ToLower().Contains(searchQuery.ToLower())
                                 || p.ProductTags.Any(pt => pt.Tag.Name.ToLower()
                                .Contains(searchQuery.ToLower())))
                                .Skip((page - 1) * (int)pageResults)
                                .Take((int)pageResults)
                                .ToListAsync();
            // Need to map back to DTO list
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            var response = new ServiceResponse<ProductPaginationResponse>()
            {
                Data = new ProductPaginationResponse
                {
                    Products = productDtos,
                    Pages = (int)numberOfPages,
                    CurrentPage = page,
                },
                Message = "Products retrieved successfully."

            };
            // Need to map to 
            return response;
        }
        public async Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page, decimal? minPrice, decimal? maxPrice)
        {
            var pageResults = 2f;
            var numberOfPages = Math.Ceiling((await FindProductsBySearchQuery(searchQuery))
                .Count / pageResults);

            // This is the query that will be used to find the products that match the search query
            // and will be used to find the
            // total number of pages for the search results pagination
            // and will be used to find the products for the requested page number
            var query = _context.Products
                .Where(p => !p.Deleted)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower())
                             || p.Description.ToLower().Contains(searchQuery.ToLower())
                             || p.ProductTags.Any(pt => pt.Tag.Name.ToLower()
                                .Contains(searchQuery.ToLower())));

            // Apply filtering by minimum and maximum price
            if (minPrice.HasValue)
            {
                query = query.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice.Value);
            }

            var products = await query
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            // Need to map back to DTO list
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            var response = new ServiceResponse<ProductPaginationResponse>()
            {
                Data = new ProductPaginationResponse
                {
                    Products = productDtos,
                    Pages = (int)numberOfPages,
                    CurrentPage = page,
                },
                Message = "Products retrieved successfully."
            };

            return response;
        }


        public async Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page, bool isAscending)
        {
            var pageResults = 2f;
            var query = _context.Products.Where(p => !p.Deleted)
                                .Include(p => p.ProductTags)
                                .ThenInclude(pt => pt.Tag)
                                .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower())
                                     || p.Description.ToLower().Contains(searchQuery.ToLower())
                                     || p.ProductTags.Any(pt => pt.Tag.Name.ToLower()
                                     .Contains(searchQuery.ToLower())));

            var numberOfPages = Math.Ceiling(await query.CountAsync() / pageResults);

            // Apply sorting based on the product name
            query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);

            var products = await query.Skip((page - 1) * (int)pageResults)
                                      .Take((int)pageResults)
                                      .ToListAsync();

            // Map to DTO list
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            var response = new ServiceResponse<ProductPaginationResponse>
            {
                Data = new ProductPaginationResponse
                {
                    Products = productDtos,
                    Pages = (int)numberOfPages,
                    CurrentPage = page,
                },
                Message = "Products retrieved successfully."
            };

            return response;
        }

        public async Task<ServiceResponse<ProductPaginationResponse>> SearchProducts(string searchQuery, int page, bool isAscending, decimal? minPrice, decimal? maxPrice)
        {
            var pageResults = 2f;
            var query = _context.Products
                .Where(p => !p.Deleted)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower())
                    || p.Description.ToLower().Contains(searchQuery.ToLower())
                    || p.ProductTags.Any(pt => pt.Tag.Name.ToLower()
                        .Contains(searchQuery.ToLower())));

            // Sorting based on isAscending parameter
            if (isAscending)
            {
                query = query.OrderBy(p => p.Name);
            }
            else
            {
                query = query.OrderByDescending(p => p.Name);
            }

            var numberOfPages = Math.Ceiling(await query.CountAsync() / pageResults);

            // Pagination
            var products = await query
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            // Need to map back to DTO list
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            var response = new ServiceResponse<ProductPaginationResponse>()
            {
                Data = new ProductPaginationResponse
                {
                    Products = productDtos,
                    Pages = (int)numberOfPages,
                    CurrentPage = page,
                },
                Message = "Products retrieved successfully."
            };

            return response;
        }

        /// <summary>
        /// Finds and retrieves a list of product DTOs based on a given search query.
        /// </summary>
        /// <remarks>
        /// This method searches for products by matching the search query with:
        /// - Product name.
        /// - Product description.
        /// - Associated tags of the product.
        /// The search is case-insensitive. The method returns a list of <see cref="ProductDto"/> corresponding to the matched products.
        /// </remarks>
        /// <param name="searchQuery">The search string used to query the products.</param>
        /// <returns>
        /// A List of <see cref="ProductDto"/> objects representing the products that match the search criteria.
        /// </returns>
        private async Task<List<ProductDto>> FindProductsBySearchQuery(string searchQuery)
        {
            var products = await _context.Products
                .Where(p => !p.Deleted)
                                .Where(p => p.Name.ToLower().Contains(searchQuery.ToLower())
                                            || p.Description.ToLower().Contains(searchQuery.ToLower())
                                            || p.Category.Name.ToLower().Contains(searchQuery.ToLower())
                                            || p.ProductTags.Any(pt => pt.Tag.Name.ToLower().Contains(searchQuery.ToLower())))
                                .ToListAsync();

            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return productDtos;
        }
        public async Task<ServiceResponse<ProductDto>> AddOrUpdateProductAsync(ProductDto productDto)
        {
            var response = new ServiceResponse<ProductDto>();
            try
            {
                // Check if the product already exists
                var existingProduct = await _context.Products.FindAsync(productDto.Id);
                var product = _mapper.Map<Product>(productDto);
                if (existingProduct != null)
                {
                    // Update existing product
                    _context.Entry(existingProduct).CurrentValues.SetValues(product);
                    await _context.SaveChangesAsync();

                    response.Data = _mapper.Map<ProductDto>(product);
                }
                else
                {
                    // Add new product
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();

                    // Update ProductTags if necessary (assuming product tags logic is similar to UserAddress)
                    foreach (var tagName in productDto.TagNames)
                    {
                        var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == tagName)
                                  ?? new Tag { Name = tagName };
                        await _context.Tags.AddAsync(tag);
                        await _context.ProductTags.AddAsync(new ProductTag { ProductId = product.Id, TagId = tag.Id });
                    }

                    response.Data = _mapper.Map<ProductDto>(product);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
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
            try
            {

            List<string> result = new();
            foreach (var product in products)
            {
                if (product.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Name);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(Char.IsPunctuation).Distinct().ToArray();
                    var words = product.Description.Split().Select(x => x.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) && !result.Contains(word))
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
            }catch (Exception ex)
            {
                return new ServiceResponse<List<string>>
                {
                    Success = false,
                    Message = $"An error occurred: {ex.Message}"
                };
            }
        }

        public Task<ServiceResponse<ProductDto>> GetProductByIdAsync(int page, int pageSize, Guid productId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// When called gets all tags.
        /// </summary>
        /// <param name="page">The page size</param>
        /// <param name="pageSize"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, Guid tagId)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();
            // Filter the products by tag first
            var query = _context.Products.Where(p => !p.Deleted).AsQueryable();
            if (tagId != Guid.Empty)
            {
                // Query products by tag
                query = query.Where(p => p.ProductTags.Any(pt => pt.TagId == tagId));
            }

            // Now get the count of products after filtering
            int totalProducts = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

            // Then get the actual page of products
            var products = await query
                .OrderBy(p => p.Name) // Optional: Default sorting by name
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                response.Success = false;
                response.StatusCode = 404;
                response.Message = "No products found for the tag.";
            }
            else
            {
                response.Data = new ProductPaginationResponse
                {
                    Products = _mapper.Map<List<ProductDto>>(products),
                    CurrentPage = page,
                    Pages = totalPages
                };
                response.Message = "Products retrieved successfully.";
            }

            return response;
        }

        public async Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(int page, int pageSize, bool isAscending, decimal? minPrice, decimal? maxPrice, Guid? categoryId)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();
            try
            {
                // Filter the products by category first
                var query = _context.Products.Where(p => !p.Deleted).AsQueryable();
                if (categoryId != Guid.Empty)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                // Apply filtering by minimum and maximum price
                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }
                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }

                // Apply sorting based on the product name
                query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);

                // Now get the count of products after filtering
                int totalProducts = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Then get the actual page of products
                var products = await query
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize)
                   .Include(p => p.ProductTags)
                   .ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found.";
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = 1,
                        Pages = 0
                    };
                }
                else
                {
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = page,
                        Pages = totalPages
                    };
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


        public async Task<ServiceResponse<ProductPaginationResponse>> GetAllProductsAsync(
            int page,
            int pageSize,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            Guid? categoryId = null)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();

            // Price Validation
            if (minPrice > maxPrice)
            {
                response.Success = false;
                response.Message = "Minimum price cannot be higher than maximum price.";
                return response;
            }

            try
            {
                var query = _context.Products.Where(p => !p.Deleted).AsQueryable();

                if (categoryId.HasValue && categoryId.Value != Guid.Empty)
                {
                    query = query.Where(p => p.CategoryId == categoryId.Value);
                }

                // Apply price filtering in a single step
                query = query.Where(p => (!minPrice.HasValue || p.Price >= minPrice.Value) &&
                                         (!maxPrice.HasValue || p.Price <= maxPrice.Value));

                // Get the total count and the products in a single database call
                var pagedData = await query
                                     .OrderBy(p => p.Name) // or however you want to sort
                                     .Skip((page - 1) * pageSize)
                                     .Take(pageSize)
                                     .Include(p => p.ProductTags)
                                     .ToListAsync();

                var totalProducts = pagedData.Count;

                // If pagedData is empty and the page is greater than 1, it might mean we are over the available pages
                if (!pagedData.Any() && page > 1)
                {
                    // Optional: adjust the logic here to better suit how you want to handle this case
                    response.Success = false;
                    response.Message = "No products found. You may be over the last page.";
                    return response;
                }

                var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

                response.Data = new ProductPaginationResponse
                {
                    Products = _mapper.Map<List<ProductDto>>(pagedData),
                    CurrentPage = page,
                    Pages = totalPages
                };

                response.Message = totalProducts > 0 ? "Products retrieved successfully." : "No products found.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = $"An error occurred: {ex.Message}";
            }

            return response;
        }


        public async Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, Guid tagId, decimal? minPrice, decimal? maxPrice)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();

            try
            {
                // Filter the products by tag first
                var query = _context.Products.Where(p => !p.Deleted).AsQueryable();
                if (tagId != Guid.Empty)
                {
                    // Query products by tag
                    query = query.Where(p => p.ProductTags.Any(pt => pt.TagId == tagId));
                }

                // Apply filtering by minimum and maximum price
                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }
                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }

                // Now get the count of products after filtering
                int totalProducts = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Then get the actual page of products
                var products = await query
                    .OrderBy(p => p.Name) // Optional: Default sorting by name
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "No products found for the tag.";
                }
                else
                {
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = page,
                        Pages = totalPages
                    };
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


        public async Task<ServiceResponse<ProductPaginationResponse>> GetProductsByTagId(int page, int pageSize, bool isAscending, Guid tagId, decimal? minPrice, decimal? maxPrice)
        {
            var response = new ServiceResponse<ProductPaginationResponse>();

            try
            {
                // Filter the products by tag first
                var query = _context.Products.Where(p => !p.Deleted).AsQueryable();
                if (tagId != Guid.Empty)
                {
                    // Query products by tag
                    query = query.Where(p => p.ProductTags.Any(pt => pt.TagId == tagId));
                }

                // Apply filtering by minimum and maximum price
                if (minPrice.HasValue)
                {
                    query = query.Where(p => p.Price >= minPrice.Value);
                }
                if (maxPrice.HasValue)
                {
                    query = query.Where(p => p.Price <= maxPrice.Value);
                }

                // Apply sorting based on the product name
                query = isAscending ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);

                // Now get the count of products after filtering
                int totalProducts = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalProducts / (double)pageSize);

                // Then get the actual page of products
                var products = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                if (products == null || !products.Any())
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "No products found for the tag.";
                }
                else
                {
                    response.Data = new ProductPaginationResponse
                    {
                        Products = _mapper.Map<List<ProductDto>>(products),
                        CurrentPage = page,
                        Pages = totalPages
                    };
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

        public Task<ServiceResponse<List<ProductDto>>> GetAdminProducts()
        {
            var response = new ServiceResponse<List<ProductDto>>();
            try
            {
                // Filter out the products that have been marked as deleted
                var products = _context.Products
                    .Where(p => !p.Deleted) // Add this line to exclude deleted products
                    .Include(p => p.ProductTags)
                    .ThenInclude(pt => pt.Tag)
                    .ToList();

                if (!products.Any())
                {
                    response.Success = false;
                    response.Message = "No products found.";
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
            return Task.FromResult(response);
        }

        public async Task<Product> GetProductById(Guid Id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(e => e.Id == Id);
            return product == null ? throw new KeyNotFoundException($"Product with Id {Id} not found.") : product;
        }
        public async Task<ServiceResponse<List<ProductDto>>> DeleteProduct(Guid productId)
        {
            var response = new ServiceResponse<List<ProductDto>>()
            {
                Data = new List<ProductDto>() // Ensure Data is always present
            };
            try
            {
                Product product = await GetProductById(productId); // Make sure this method exists and fetches the product by id
                if (product == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Product not found.";
                    response.StatusCode = 404; // Not Found
                    return response;
                }

                if (product.Deleted)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Product already deleted.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }

                product.Deleted = true; // Soft delete the product by setting the Deleted property to true
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Failed to delete product.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }

                response = await GetAdminProducts(); // Refresh the list of products to reflect the deletion
                response.Success = true;
                response.Message = "Product deleted successfully.";
                response.StatusCode = 200; // OK
                return response;
            }
            catch (Exception ex)
            {
                // Log the exception details here, including 'ex' information

                response.Success = false;
                response.Message = "Server error occurred while deleting the product: " + ex.Message;
                response.StatusCode = 500; // Internal Server Error
                return response;
            }
        }


        public async Task<ServiceResponse<List<ProductDto>>> UpdateProduct(ProductDto productDto)
        {
            // Validate the product name
            if (!Regex.IsMatch(productDto.Name, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$"))
            {
                return new ServiceResponse<List<ProductDto>>
                {
                    Success = false,
                    Message = "Invalid product name. Only letters are allowed.",
                    Data = null,
                    StatusCode = 400 // Bad Request
                };
            }
            // get the id of the product so that we know we can update the name of the product with the same id
            // get the id of the product based on the dto id
            var productId = await _context.Products.Where(p => p.Id == productDto.Id).Select(p => p.Id).FirstOrDefaultAsync();


            // Check for name uniqueness and non-deleted status but only 
            var isNameUsed = await _context.Products
                                .AnyAsync(p => p.Name == productDto.Name && p.Id != productDto.Id && !p.Deleted);
            if (isNameUsed)
            {
                return new ServiceResponse<List<ProductDto>>
                {
                    Success = false,
                    Message = "Product name already exists.",
                    Data = null,
                    StatusCode = 400 // Bad Request
                };
            }

            // Check if the product exists
            var dbProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productDto.Id);
            if (dbProduct == null)
            {
                return new ServiceResponse<List<ProductDto>>
                {
                    Success = false,
                    Message = "Product not found.",
                    Data = null,
                    StatusCode = 404 // Not Found
                };
            }

            // Update product properties
            dbProduct.Name = productDto.Name;
            dbProduct.Description = productDto.Description;
            dbProduct.Price = productDto.Price;
            dbProduct.Visible = productDto.Visible;
            dbProduct.Deleted = productDto.Deleted;
            dbProduct.CategoryId = productDto.CategoryId;
            dbProduct.StockQuantity = productDto.StockQuantity;
            dbProduct.DateModified = DateTime.UtcNow;
            // Add or update additional properties as needed

            // Handling the images, assuming ProductDto.Images is the source of truth
            dbProduct.ImagesJson = JsonConvert.SerializeObject(productDto.Images);

            try
            {
                await _context.SaveChangesAsync();
                var updatedProducts = await GetAdminProducts(); // Fetch updated list of products
                return new ServiceResponse<List<ProductDto>>
                {
                    Success = true,
                    Message = "Product updated successfully.",
                    Data = updatedProducts.Data,
                    StatusCode = 200 // OK
                };
            }
            catch (Exception ex)
            {
                // Log the exception details here, including 'ex' information

                return new ServiceResponse<List<ProductDto>>
                {
                    Success = false,
                    Message = "An error occurred while updating the product: " + ex.Message,
                    Data = null,
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public async Task<ServiceResponse<bool>> AddProduct(ProductDto productDto)
        {
            // Validate the product name
            if (!Regex.IsMatch(productDto.Name, @"^[a-zA-ZéèêëîïôœùûüçÉÈÊËÎÏÔŒÙÛÜÇ ]+$"))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Invalid product name. Only letters are allowed.",
                    Data = false,
                    StatusCode = 400 // Bad Request
                };
            }

            // Check for name uniqueness
            var isNameUsed = await _context.Products
                                .AnyAsync(p => p.Name == productDto.Name && !p.Deleted);

            if (isNameUsed)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Product name already exists.",
                    Data = false,
                    StatusCode = 400 // Bad Request
                };
            }

            var response = new ServiceResponse<bool>
            {
                Data = false
            };

            try
            {
                Product newProduct = _mapper.Map<Product>(productDto);
                newProduct.Editing = newProduct.IsNew = false; // Set defaults for new products
                _context.Products.Add(newProduct);
                var result = await _context.SaveChangesAsync();

                if (result <= 0)
                {
                    response.Success = false;
                    response.Message = "Failed to add product.";
                    response.StatusCode = 400; // Bad Request
                    return response;
                }
                else
                {
                    response.Data = true;
                    response.Success = true;
                    response.Message = "Product added successfully.";
                    response.StatusCode = 201; // Created
                    return response;
                }
            }
            catch (Exception ex)
            {
                // Log the exception details here, including 'ex' information

                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "An unexpected server error occurred while adding the product: " + ex.Message,
                    Data = false,
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public Task<ServiceResponse<ProductDto>> GetAdminProductById(Guid productId)
        {
            var response = new ServiceResponse<ProductDto>();
            try
            {
                var product = _context.Products
                    .Include(p => p.ProductTags)
                    .ThenInclude(pt => pt.Tag)
                    .FirstOrDefault(p => p.Id == productId);

                if (product == null)
                {
                    response.Success = false;
                    response.Message = "Product not found.";
                    response.StatusCode = 404; // Not Found
                    return Task.FromResult(response);
                }

                var productDto = _mapper.Map<ProductDto>(product);
                response.Data = productDto;
                response.Message = "Product retrieved successfully.";
                response.StatusCode = 200; // OK
                return Task.FromResult(response);
            }
            catch (Exception ex)
            {
                // Log the exception details here, including 'ex' information

                response.Success = false;
                response.Message = "An unexpected server error occurred while retrieving the product: " + ex.Message;
                response.StatusCode = 500; // Internal Server Error
                return Task.FromResult(response);
            }   
        }
    }
}
