using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Purchase.Infrastructure.Persistence
{
    public class POSPurchaseDbContext :DbContext
    {
        public POSPurchaseDbContext(DbContextOptions<POSPurchaseDbContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationships and constraints for PurchaseItem and PurchaseReturnItem
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(POSPurchaseDbContext).Assembly);
        }
    }
}
