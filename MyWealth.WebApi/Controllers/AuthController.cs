using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWealth.Business.Operations.User;
using MyWealth.Business.Operations.User.Dtos;
using MyWealth.WebApi.Models;

namespace MyWealth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var registerDto = new RegisterDto
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
            };

            var result = await _authService.Register(registerDto);

            if(!result.IsSucceed)
            {
                return StatusCode(500, result.Message.ToString());
            }

            return Ok(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var loginDto = new LoginDto
            {
                Username = request.Username,
                Password = request.Password,
            };

            var result = await _authService.Login(loginDto);

            if (!result.IsSucceed)
                return Unauthorized(result.Message);

            return Ok(result);
        }


    }
}
