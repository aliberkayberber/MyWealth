using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.Operations.User.Dtos
{
    public class UserInfoDto
    {
        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string Token { get; set; }
    }
}
