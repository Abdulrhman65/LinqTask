using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using linq1.Models;

namespace linq1.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=App1;Trusted_Connection=true;TrustServerCertificate=True;");
        }
    }
}
