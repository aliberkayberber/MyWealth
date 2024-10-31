using MyWealth.Business.DataProtection;
using MyWealth.Business.Operations.User.Dtos;
using MyWealth.Business.Types;
using MyWealth.Data.Entities;
using MyWealth.Data.Enums;
using MyWealth.Data.Repositories;
using MyWealth.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _dataProtection;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection dataProtection)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _dataProtection = dataProtection;
        }

        public ServiceMessage<UserInfoDto> Login(LoginDto user)
        {
            var userEntity = _userRepository.Get(x => x.Email.ToLower() == user.Email.ToLower());

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Username or password is incorrect"
                };
            }

            var unproctectPassword = _dataProtection.UnProtect(userEntity.Password);

            if (unproctectPassword == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Message = "Login",
                    Data = new UserInfoDto
                    {
                        Email = userEntity.Email,
                        UserName = userEntity.UserName,
                        UserType = userEntity.UserType,
                    }
                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message= "Username or password is incorrect"
                };
            }


        }

        public async Task<ServiceMessage> Register(RegisterDto user)
        {
            var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == user.Email.ToLower());

            if(hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSucceed = true,
                    Message = "Email already exist"
                };
            }


            var userEntity = new UserEntity
            {
                Email = user.Email,
                UserName = user.UserName,
                Password = _dataProtection.Protect(user.Password),
                UserType = UserType.User,
            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("User dont created");
            }


            return new ServiceMessage
            {
                IsSucceed = true,
            };


        }
    }
}
