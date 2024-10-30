using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWealth.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Data.Context
{
    public class MyWealthDbContext : IdentityDbContext<AppUser>
    {
        public MyWealthDbContext(DbContextOptions<MyWealthDbContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            //modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName ="ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName ="USER"
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<StockEntity> Stocks  => Set<StockEntity>();

        public DbSet<CommentEntity> Comments => Set<CommentEntity>();

        //public DbSet<UserEntity> Users => Set<UserEntity>();

        public DbSet<PortfolioEntity> Portfolios => Set<PortfolioEntity>();



    }
}
