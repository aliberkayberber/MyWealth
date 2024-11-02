using MyWealth.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; } // user email

        public string Password { get; set; } // user password

        public string UserName { get; set; } // user username

        public UserType UserType { get; set; } // user type 

        public List<PortfolioEntity> Portfolios { get; set; }

        public List<CommentEntity> Comments { get; set; }
    }
}
