using ALLAH.Application.interfaces.Contexts;
using ALLAH.Common.Roles;
using ALLAH.Domain.Entities.Carts;
using ALLAH.Domain.Entities.Finances;
using ALLAH.Domain.Entities.HomePages;
using ALLAH.Domain.Entities.Orders;
using ALLAH.Domain.Entities.Products;
using ALLAH.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALLAH.Persistance.Context
{
    public  class DataBaseContext : DbContext , IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options): base(options) { }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set;  }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<HomePageImages> HomePagesImages { get; set; }      
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }  
        
        public DbSet<RequestPay> requestPays { get ; set; }

       
        
        public DbSet<Order> Orders { get ; set ; }
        public DbSet<OrderDetail> OrderDetails { get ; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Order>()
                .HasOne(p=>p.User)
                .WithMany(p=>p.orders)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(p => p.RequestPay)
                .WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.NoAction);


           SeedData(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(p=>p.Email).IsUnique();

           ApplyQueryFilter(modelBuilder);
        }
        public void ApplyQueryFilter (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomePageImages>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItem>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<RequestPay>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Order>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<OrderDetail>().HasQueryFilter(p => !p.IsRemoved);
        }
         private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRoles.Operator) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRoles.Customer) });

        }
    }
}
