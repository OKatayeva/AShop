using System;
using AShop_Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AShop_Data
{
    public class AshopDB : IdentityDbContext
    {
        public AshopDB (DbContextOptions<AshopDB> options): base(options)
        {

        }

        public DbSet <Category> Category { get; set; }

        public DbSet<ApplicationType> ApplicationType { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<OrderHeader> OrderHeader { get; set; }

        public DbSet<OrderDetail> OrderDetail { get; set; }

        public DbSet<Brand> Brand { get; set; }
    }
}
