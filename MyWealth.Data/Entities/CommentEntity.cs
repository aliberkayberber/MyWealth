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
        public int StockId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty ;

        

        // Relations property
        public StockEntity Stock { get; set; }
        //public int UserId { get; set; }
        //public UserEntity? User { get; set; }

    }

    public class CommentConfiguration: BaseConfiguration<CommentEntity>
    {
        public override void Configure(EntityTypeBuilder<CommentEntity> builder)
        {
            builder.Property(x => x.Content)
                   .IsRequired();

            builder.Property(x => x.Title)
                   .HasMaxLength(300)
                   .IsRequired();

            base.Configure(builder);
        }
    }

}
