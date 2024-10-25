using Microsoft.EntityFrameworkCore;
using MyWealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Context
{
    public class MyWealthDbContext : DbContext
    {
        public MyWealthDbContext(DbContextOptions<MyWealthDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<StockEntity> Stocks  => Set<StockEntity>();

        public DbSet<CommentEntity> Comments => Set<CommentEntity>();

        public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<PortfolioEntity> Portfolios => Set<PortfolioEntity>();



    }
}
