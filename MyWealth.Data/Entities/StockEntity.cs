using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Entities
{
    public class StockEntity : BaseEntity
    {
        public string Symbol { get; set; } = string.Empty; // stock symbol

        public string CompanyName { get; set; } = string.Empty;  // stock CompanyName

        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; } // stock Purchase

        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; } // Divdend

        public string Industry { get; set; } = string.Empty; // stock Industry

        public long MarketCap { get; set; } // stock MarketCap

        // Relations property

        public List<CommentEntity>? Comments { get; set; } = new List<CommentEntity>();

        public List<PortfolioEntity>? Portfolios { get; set; } = new List<PortfolioEntity>();
    }


    public class StockConfiguration: BaseConfiguration<StockEntity>
    {
        public override void Configure(EntityTypeBuilder<StockEntity> builder)
        {
            builder.Property(x => x.Symbol)
                   .IsRequired();

            builder.Property(x => x.CompanyName)
                   .IsRequired();

            builder.Property(x => x.Purchase)
                   .IsRequired();

            builder.Property(x => x.LastDiv)
                   .IsRequired();

            builder.Property(x => x.Industry)
                   .IsRequired();

            builder.Property (x => x.MarketCap)
                   .IsRequired();


            base.Configure(builder);
        }
    }
}
