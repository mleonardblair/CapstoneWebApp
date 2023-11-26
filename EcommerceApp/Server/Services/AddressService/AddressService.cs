using AutoMapper;
using EcommerceApp.Server.Models;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Server.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly AppDbContext _context;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public AddressService(AppDbContext context, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<AddressDto>>> GetAllAddressesAsync()
        {
            var response = new ServiceResponse<List<AddressDto>>();
            try
            {
                var userId = _authService.GetUserId();
                var userAddresses = await _context.UserAddresses
                                                 .Where(ua => ua.ApplicationUserId == userId)
                                                 .Include(ua => ua.Address)
                                                 .ToListAsync();

                // Map each Address to AddressDto
                response.Data = userAddresses.Select(ua => _mapper.Map<AddressDto>(ua.Address)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<AddressDto>> GetAddressByIdAsync(Guid addressId)
        {
            var response = new ServiceResponse<AddressDto>();
            try
            {
                var address = await _context.Addresses.FindAsync(addressId);
                if (address != null)
                {
                    // This method converts to dto
                    var addressDto = _mapper.Map<AddressDto>(address); // 
                    response.Data = addressDto;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Address not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<AddressDto>> AddOrUpdateAddressAsync(AddressDto addressDto)
        {
            var userId = _authService.GetUserId();
            var response = new ServiceResponse<AddressDto>();
            try
            {

                // Check if the address already exists
                var existingAddress = await _context.Addresses.FindAsync(addressDto.Id);
                var address = _mapper.Map<Address>(addressDto);
                if (existingAddress != null)
                {

                    // Update existing address
                    _context.Entry(existingAddress).CurrentValues.SetValues(address);
                    await _context.SaveChangesAsync();
       
                    // Update UserAddress if necessary
                    var userAddress = await _context.UserAddresses.FirstOrDefaultAsync(ua => ua.AddressId == address.Id);
                    if (userAddress != null)
                    {
                        userAddress.Address = address;
                        _context.UserAddresses.Update(userAddress);
                    }

                    response.Data = _mapper.Map<AddressDto>(address);
                }
                else
                {
                    // Add new address
                    await _context.Addresses.AddAsync(address);
                    await _context.SaveChangesAsync();

                    // Create a new UserAddress entry
                    var newUserAddress = new UserAddress
                    {
                        AddressId = address.Id,
                        ApplicationUserId = userId,
                        AddressType = AddressType.Shipping, // Assuming you have a default type or similar
                        DateCreated = DateTime.UtcNow
                    };

                    await _context.UserAddresses.AddAsync(newUserAddress);
                    response.Data = _mapper.Map<AddressDto>(address);
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


    }
}
