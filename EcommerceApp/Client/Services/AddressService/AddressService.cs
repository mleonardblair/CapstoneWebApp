using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;

        public AddressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<AddressDto> AddOrUpdateAddressAsync(AddressDto address)
        {
            var response = await _httpClient.PostAsJsonAsync("api/address", address);
            if (response.IsSuccessStatusCode)
            {
                var serviceResponse = await response.Content.ReadFromJsonAsync<ServiceResponse<AddressDto>>();
                return serviceResponse.Data;
            }
            else
            {
                // Handle the error, log it, or throw an exception with more details.
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error calling API: {errorContent}");
            }
        }


        public async Task<AddressDto> GetAddressByIdAsync(Guid addressId)
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<AddressDto>>($"api/address/{addressId}");
            return response.Data;
        }

        public async Task<List<AddressDto>> GetAddressesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ServiceResponse<List<AddressDto>>>("api/address");
            return response.Data;
        }
    }
}
