using POS.Product.Application.Interfaces.BrandInterfaces;
using POS.Product.Application.Interfaces.CategoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        IBrandRepository Brands { get; }
        Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}

