using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Dtos.Category
{
    public class CreateCategoryRequest
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid? ParentCategoryId { get; set; }
    }
}
