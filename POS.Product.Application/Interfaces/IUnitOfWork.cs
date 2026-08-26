using POS.Product.Application.Interfaces.BrandInterfaces;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using POS.Product.Application.Interfaces.ProductInterfaces;
using POS.Product.Application.Interfaces.UnitInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IBrandRepository Brands { get; }
        IUnitRepository Units { get; }
        public IProductRepository Products { get; }

        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}

