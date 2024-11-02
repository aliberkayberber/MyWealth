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
            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());

            modelBuilder.Entity<SettingEntity>().HasData(
        new SettingEntity
         {
             Id = 1, // just admin  
             MaintenanceMode = false
         });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<StockEntity> Stocks  => Set<StockEntity>(); // Stocks entity set database

        public DbSet<CommentEntity> Comments => Set<CommentEntity>(); // Comments entity set database

        public DbSet<UserEntity> Users => Set<UserEntity>();  // Users entity set database

        public DbSet<PortfolioEntity> Portfolios => Set<PortfolioEntity>(); // Portfolios entity set database

        public DbSet<SettingEntity> Settings => Set<SettingEntity>(); // Settings entity set database

    }
}
