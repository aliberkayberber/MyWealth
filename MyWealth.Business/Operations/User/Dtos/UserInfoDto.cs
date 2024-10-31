﻿using MyWealth.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.User.Dtos
{
    public class UserInfoDto
    {
        public int Id { get; set; } 

        public string UserName { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }

        //public string Token { get; set; }
    }
}
