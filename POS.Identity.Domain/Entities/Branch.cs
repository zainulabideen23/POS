using System;

public class Branch
{
    // Primary Key
    public Guid Id { get; set; }

    // Foreign Key mapping to Tenants table
    public Guid TenantId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string? Code { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Navigation Property: Each branch belongs to one Tenant
    public Tenant Tenant { get; set; } = null!;
}
