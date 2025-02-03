using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Type> Types => Set<Type>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Manager> Managers => Set<Manager>();
        public DbSet<Buyer> Buyers => Set<Buyer>();
        public DbSet<Selling> Sellings => Set<Selling>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-005JK2O\SQLEXPRESS;Database=stationery;TrustServerCertificate=True;Trusted_Connection=True;");
        }
    }
}
