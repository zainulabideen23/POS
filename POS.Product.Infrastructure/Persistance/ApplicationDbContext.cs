using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using POS.Product.Domain.Entities;

namespace POS.Product.Infrastructure.Persistance
{
    public class ApplciationDbContext : DbContext
    {
        public ApplciationDbContext(DbContextOptions<ApplciationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Brand> Brands => Set<Brand>();

        public DbSet<Unit> Units => Set<Unit>();

        public DbSet<product> Products => Set<product>();

        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();

        public DbSet<ProductBarcode> ProductBarcodes => Set<ProductBarcode>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplciationDbContext).Assembly);
        }
    }
}
