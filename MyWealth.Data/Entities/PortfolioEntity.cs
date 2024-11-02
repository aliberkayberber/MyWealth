using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Entities
{
    public class PortfolioEntity : BaseEntity
    {
        public int UserId { get; set; } // portfolio owner user id

        public int StockId { get; set; } // portfolio stocks id

        public UserEntity User { get; set; } // portfolio owner user

        public StockEntity Stock { get; set; } // portfolio stocks
    }

    public class PortfolioConfiguration: BaseConfiguration<PortfolioEntity>
    {
        public override void Configure(EntityTypeBuilder<PortfolioEntity> builder)
        {
            builder.Ignore(x => x.Id);

            builder.HasKey("UserId", "StockId"); // composite key


            base.Configure(builder);
        }
    }
}
