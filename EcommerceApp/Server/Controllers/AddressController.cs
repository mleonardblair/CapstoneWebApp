using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<AddressDto>>>> GetAllAddresses()
        {
            return Ok(await _addressService.GetAllAddressesAsync());
        }
        [HttpGet("{addressId}")]
        public async Task<ActionResult<ServiceResponse<AddressDto>>> GetAddressByIdAsync(Guid addressId)
        {
            return Ok(await _addressService.GetAddressByIdAsync(addressId));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddressDto>>>> AddOrUpdateAddress(AddressDto address)
        {
            return Ok(await _addressService.AddOrUpdateAddressAsync(address));
        }
    }
}
