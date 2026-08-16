using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Category
{
    public class CategoryTreeResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid? ParentCategoryId { get; set; }

        public bool IsActive { get; set; }

        public List<CategoryTreeResponse> subCategories { get; set; }
            = new();
    }
}
