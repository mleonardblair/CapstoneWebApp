using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Server.Services.AddressService
{
    public interface IAddressService
    {
        Task<ServiceResponse<List<AddressDto>>> GetAllAddressesAsync();
        Task<ServiceResponse<AddressDto>> GetAddressByIdAsync(Guid addressId);
        Task<ServiceResponse<AddressDto>> AddOrUpdateAddressAsync(AddressDto address);
    }
}
