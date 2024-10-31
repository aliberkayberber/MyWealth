using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyWealth.Business.Operations.User;
using MyWealth.Business.Operations.User.Dtos;
using MyWealth.WebApi.Jwt;
using MyWealth.WebApi.Models;

namespace MyWealth.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Regtister(RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerDto = new RegisterDto
            {
                Email = request.Email,
                UserName = request.UserName,
                Password = request.Password,
            };

            var result = await _userService.Register(registerDto);

            if(result.IsSucceed)
            {
                return Ok();
            }
            else return BadRequest(result.Message);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginDto = new LoginDto
            {
                Email = request.Email,
                Password = request.Password,
            };


            var result =  _userService.Login(loginDto);

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }

            // jwt
            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateJwtToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Message = "Giriş Başarılı ile tamalandı",
                Token = token,
            });


        } 

    }
}
