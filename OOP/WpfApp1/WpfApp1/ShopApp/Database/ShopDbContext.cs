using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using WpfApp1.ShopApp.Model;

namespace WpfApp1.ShopApp.Database
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Client" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Техника" },
                new Category { Id = 2, Name = "Еда" },
                new Category { Id = 3, Name = "Одежда" },
                new Category { Id = 4, Name = "Без категории" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Username = "Admin",
                    Password = "sih",
                    RoleId = 1
                }
            );
        }
    }
}