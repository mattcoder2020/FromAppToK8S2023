using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProductService.Commands;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductService.SQLiteDB
{
    public class StoreDBContext:DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().hasHasOne(e=>e.ProductCategory).
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
            {
                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                    var dateTimeProperties = entityType.ClrType.GetProperties()
                        .Where(p => p.PropertyType == typeof(DateTimeOffset));

                    foreach (var property in properties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
                    }

                    foreach (var property in dateTimeProperties)
                    {
                        modelBuilder.Entity(entityType.Name).Property(property.Name)
                            .HasConversion(new DateTimeOffsetToBinaryConverter());
                    }
                }
            }
        }
    }
}
