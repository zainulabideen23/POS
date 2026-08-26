using Microsoft.EntityFrameworkCore;
using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.BrandInterfaces;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Application.Interfaces.ProductInterfaces;
using POS.Product.Application.Interfaces.UnitInterfaces;
using POS.Product.Domain.Entities;
using POS.Product.Infrastructure.Persistance;
using POS.Product.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplciationDbContext _db;

        public ICategoryRepository Categories { get; }
        public IBrandRepository Brands { get; }

        public IUnitRepository Units { get; }
        public IProductRepository Products { get; }


        public UnitOfWork(
            ApplciationDbContext db)
        {
            _db = db;
            Categories = new CategoryRepository(_db);
            Brands = new BrandRepository(_db);
            Units = new UnitRepository(_db);
            Products = new ProductRepository(_db);
        }

        public async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _db.SaveChangesAsync(
                cancellationToken);
        }
    }
}
