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
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _context;

        public UserController(IAuthService authService, AppDbContext context)
        {
            _authService = authService;
            _context = context;
        }

        // Get the user by guid id
        [Authorize]
        [HttpGet("admin/{Id}")]
        public async Task<ActionResult<ServiceResponse<AppUserDto>>> GetUserById(Guid Id)
        {

            return Ok(await _authService.GetUserById(Id));

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public async Task<ActionResult<ServiceResponse<List<AppUserDto>>>> GetAllUserAdmin()
        {

            return Ok(await _authService.GetAllUserAdmin());

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser(AppUserDto appUser)
        {

            return Ok(await _authService.UpdateUser(appUser));

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddUser(AppUserDto appUser)
        {

            return Ok(await _authService.AddUser(appUser));

        }



    }
}
