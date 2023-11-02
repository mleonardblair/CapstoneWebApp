using EcommerceApp.Shared.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EcommerceApp.Server.Models
{
    public class ProductDtoModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var formCollection = bindingContext.ActionContext.HttpContext.Request.Form;
            var productDto = new ProductDto();

            // Map each property from formCollection to your ProductDto
            if (formCollection.ContainsKey("Id") && !string.IsNullOrWhiteSpace(formCollection["Id"]))
            {
                productDto.Id = Guid.Parse(formCollection["Id"]);
            }
            productDto.Name = formCollection["Name"];
            productDto.Description = formCollection["Description"];
            productDto.ImageURI = formCollection["ImageURI"];
            productDto.Price = decimal.Parse(formCollection["Price"]);
            productDto.StockQuantity = int.Parse(formCollection["StockQuantity"]);
            productDto.CategoryId = Guid.Parse(formCollection["CategoryId"]);

            // ... continue mapping all other fields ...

            bindingContext.Result = ModelBindingResult.Success(productDto);
            return Task.CompletedTask;
        }
    }
}
