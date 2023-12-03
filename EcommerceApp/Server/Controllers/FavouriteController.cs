using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Server.Services.FavouriteService;
using EcommerceApp.Shared.DTOs;
using System;
using System.Threading.Tasks;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavouriteController : ControllerBase
    {
        private readonly IFavouriteService _favouriteService;

        public FavouriteController(IFavouriteService favouriteService)
        {
            _favouriteService = favouriteService;
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<ServiceResponse<List<ProductDto>>>> GetFavouritesByUserId(Guid userId)
        {
            var result = await _favouriteService.GetFavouritesByUserId(userId);
            return Ok(result);
         }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FavouriteProductResponse>>> AddFavourite(FavouriteProductResponse favouriteDto)
        {
            var result = await _favouriteService.AddFavouriteAsync(favouriteDto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<FavouriteProductResponse>>> GetFavouriteById(Guid id)
        {
            var result = await _favouriteService.GetFavouriteByIdAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<FavouriteProductResponse>>>> GetAllFavourites()
        {
            var result = await _favouriteService.GetAllFavouritesAsync();
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<FavouriteProductResponse>>> UpdateFavourite(FavouriteDto favouriteDto)
        {
            var result = await _favouriteService.UpdateFavouriteAsync(favouriteDto);
            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteFavourite(Guid id)
        {
            var result = await _favouriteService.DeleteFavouriteAsync(id);
            return result.Success ? Ok(result) : NotFound(result);
        }
    }

}
