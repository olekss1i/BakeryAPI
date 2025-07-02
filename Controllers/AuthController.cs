using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BakeryAPI.Services;
using BakeryAPI.DTOs;
using System.Threading.Tasks;

namespace BakeryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
    }
}
