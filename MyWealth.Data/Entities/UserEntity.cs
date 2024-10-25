using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public UserType UserType { get; set; }

        public List<PortfolioEntity> Portfolio { get; set; }

        public List<CommentEntity> Comments { get; set; }

    }

    public class UserEntityConfiguration:BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.Property(x => x.UserName)
                   .IsRequired()
                   .HasMaxLength(128);


            base.Configure(builder);
        }
    }
}
