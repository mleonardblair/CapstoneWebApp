using EcommerceApp.Shared.DTOs;

namespace EcommerceApp.Client.Services.AddressService
{
    public interface IAddressService
    {
        Task<List<AddressDto>> GetAddressesAsync();
        Task<AddressDto> GetAddressByIdAsync(Guid addressId);
        Task<AddressDto> AddOrUpdateAddressAsync(AddressDto address);
    }
}
