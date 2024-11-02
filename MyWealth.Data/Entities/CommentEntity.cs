using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Entities
{
    public class CommentEntity :BaseEntity
    {
        public int StockId { get; set; } // id of the stock being commented on
        public int UserId { get; set; } // id of the user who commented
        public string Title { get; set; } = string.Empty; // comment title

        public string Content { get; set; } = string.Empty ; // title content

        

        // Relations property
        public StockEntity Stock { get; set; } // Commented stock

        public UserEntity User { get; set; }  // Commenting user

    }

    public class CommentConfiguration: BaseConfiguration<CommentEntity>
    {
        public override void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.Ignore(x => x.Id); 

            builder.HasKey("UserId", "StockId"); // composite key

            builder.Property(x => x.Content)
                   .IsRequired();

            builder.Property(x => x.Title)
                   .HasMaxLength(300)
                   .IsRequired();



            base.Configure(builder);
        }
    }

}
