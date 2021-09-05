using System;
using AShop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AShop.Data
{
    public class AshopDB : IdentityDbContext
    {
        public AshopDB (DbContextOptions<AshopDB> options): base(options)
        {

        }

        public DbSet <Category> Category { get; set; }

        public DbSet<ApplicationType> ApplicationType { get; set; }

        public DbSet<Product> Product { get; set; }
    }
}
