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
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public UserType UserType { get; set; }

        public List<PortfolioEntity> Portfolios { get; set; }
    }
}
