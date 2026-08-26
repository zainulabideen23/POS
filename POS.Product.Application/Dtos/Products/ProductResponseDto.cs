
namespace POS.Product.Application.Dtos.Products;

public class ProductResponseDto
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    public Guid BrandId { get; set; }

    public string BrandName { get; set; } = string.Empty;

    public Guid UnitId { get; set; }

    public string UnitName { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? SKU { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SellingPrice { get; set; }

    public Guid? TaxRateId { get; set; }

    public decimal ReorderLevel { get; set; }

    public bool TrackInventory { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

}