using Microsoft.EntityFrameworkCore;
using POS.Organization.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Infrastructure.Persistence
{
    public class POSOrganzationDbContext : DbContext
    {
        public POSOrganzationDbContext(DbContextOptions<POSOrganzationDbContext> options) : base(options)
        {

        }

        public DbSet<Warehouse> Warehouses { get; set; }

        public DbSet<POSRegister> POSRegisters { get; set; }
        public DbSet<RegisterSession> RegisterSessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Apply configurations from the current assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(POSOrganzationDbContext).Assembly);
        }
    }
}
