using EcommerceApp.Server.Models;
using EcommerceApp.Server.Services.AuthService;
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        /// <summary>
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// 


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<AppUserDto>>> GetUser(Guid id)
        {
            var response = await _authService.GetUserById(id);
            if(response != null)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
          
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(UserRegister request)
        {
            // When called from the Blazor WASM app, the request body will contain the user's email and password.
            // The AuthService will then attempt to register the user with the provided credentials.
            var response = await _authService.Register(
                                        new ApplicationUser
                                        {
                                            Id = Guid.NewGuid(),
                                            Email = request.Email,
                                            ShoppingCartId = Guid.NewGuid()
                                        },
                                        request.Password);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
        {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(UserChangePassword changeRequest)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var response = await _authService.ChangePassword(userId, changeRequest.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<ActionResult<ServiceResponse<bool>>> UpdateUser( AppUserDto userDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.UpdateUser(userId, userDto.Email, userDto.FirstName, userDto.LastName);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response);
        }
    }
}
