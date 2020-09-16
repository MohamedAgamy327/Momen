using Data.SeedData;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Data.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Seed();
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<VendorUser> VendorsUsers { get; set; }
        public DbSet<VendorReject> VendorsRejects { get; set; }
        public DbSet<VendorPicture> VendorsPictures { get; set; }

    }
}
