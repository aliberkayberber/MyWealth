using MyWealth.Business.Operations.User.Dtos;
using MyWealth.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> Register(RegisterDto registerDto); // for register

        ServiceMessage<UserInfoDto> Login(LoginDto user); // for login
    }
}
