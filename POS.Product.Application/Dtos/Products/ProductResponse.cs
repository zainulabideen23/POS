
namespace POS.Product.Application.Dtos.Products;

public class ProductResponse
{
    public Guid Id { get; set; }

    public Guid TenantId { get; set; }

    public Guid CategoryId { get; set; }

    public Guid BrandId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal CostPrice { get; set; }

    public decimal SellingPrice { get; set; }

    public bool IsActive { get; set; }
}