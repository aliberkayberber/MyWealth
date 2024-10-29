using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWealth.Business.Operations.User.Dtos;
using MyWealth.Business.Types;
using MyWealth.Data.Entities;
using MyWealth.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.User
{
    public class AuthManager : IAuthService
    {
        //private readonly IRepository<AppUser> _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;

        public AuthManager(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
        }

        public async Task<ServiceMessage<UserInfoDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());

            if (user == null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "User not found Unauthorized"
                };
            }

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password,false);

            if(!result.Succeeded)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed =false,
                    Message = "Username not found and/or password incorrect"
                };
            }

            return new ServiceMessage<UserInfoDto>
            {
                IsSucceed=true,
                Message = "Login completed successfully",
                Data = new UserInfoDto
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = _tokenService.CreateToken(user)
                }
            };

        }

        public async Task<ServiceMessage<UserInfoDto>> Register(RegisterDto registerDto)
        {
            try
            {

                var appUser = new AppUser
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return new ServiceMessage<UserInfoDto>
                        {
                            IsSucceed = true,
                            Message = "User added",
                            Data =new UserInfoDto
                            {
                                Email = appUser.Email,
                                UserName = appUser.UserName,
                                Token = _tokenService.CreateToken(appUser)
                            }
                            
                        };
                        /*Ok(
                        new NewUserDto
                        {
                            UserName = appUser.UserName,
                            Email = 
                      Token = _tokenService.CreateToken(appUser)
                        }

                    );
                        */              
                    }
                    else
                    {
                        return new ServiceMessage<UserInfoDto>
                        {
                            IsSucceed = false,
                            Message = "User roles not found"
                        };
                            
                       // StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return new ServiceMessage<UserInfoDto>
                    {
                        IsSucceed = false,
                        Message = "User not created"
                    };
                        //StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = e.Message
                };
                    
                    //StatusCode(500, e);
            }
        }
    
    }
}
