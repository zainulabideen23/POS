
using POS.Identity.Application.DTOs;
using POS.Identity.Application.Interfaces;

namespace POS.Application.Services;

public class TenantService
{
    private readonly IUnitOfWork _unitOfWork;

    public TenantService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<TenantResponseDto>>
        GetAllAsync()
    {
        var tenants =
            await _unitOfWork.Tenants.GetAllAsync();

        return tenants.Select(MapToResponse);
    }

    public async Task<TenantResponseDto?>
        GetByIdAsync(Guid id)
    {
        var tenant =
            await _unitOfWork.Tenants
                .GetTenantWithBranchesAsync(id);

        if (tenant == null)
            return null;

        return MapToResponse(tenant);
    }

    public async Task<TenantResponseDto>
        CreateAsync(CreateTenantDto dto)
    {
        var tenant = new Tenant
        {
            Name = dto.Name,
            LegalName = dto.LegalName,
            TaxNumber = dto.TaxNumber,
            Email = dto.Email,
            Phone = dto.Phone,
            Address = dto.Address,
            LogoUrl = dto.LogoUrl,
            Currency = dto.Currency,
            Timezone = dto.Timezone
        };

        await _unitOfWork.Tenants.AddAsync(tenant);

        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(tenant);
    }

    public async Task<bool>
        UpdateAsync(Guid id, UpdateTenantDto dto)
    {
        var tenant =
            await _unitOfWork.Tenants.GetByIdAsync(id);

        if (tenant == null)
            return false;

        tenant.Name = dto.Name;
        tenant.LegalName = dto.LegalName;
        tenant.TaxNumber = dto.TaxNumber;
        tenant.Email = dto.Email;
        tenant.Phone = dto.Phone;
        tenant.Address = dto.Address;
        tenant.LogoUrl = dto.LogoUrl;
        tenant.Currency = dto.Currency;
        tenant.Timezone = dto.Timezone;
        tenant.IsActive = dto.IsActive;
        tenant.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Tenants.Update(tenant);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tenant =
            await _unitOfWork.Tenants.GetByIdAsync(id);

        if (tenant == null)
            return false;

        _unitOfWork.Tenants.Delete(tenant);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private static TenantResponseDto MapToResponse(
        Tenant tenant)
    {
        return new TenantResponseDto
        {
            Id = tenant.Id,
            Name = tenant.Name,
            LegalName = tenant.LegalName,
            TaxNumber = tenant.TaxNumber,
            Email = tenant.Email,
            Phone = tenant.Phone,
            Address = tenant.Address,
            LogoUrl = tenant.LogoUrl,
            Currency = tenant.Currency,
            Timezone = tenant.Timezone,
            IsActive = tenant.IsActive,
            CreatedAt = tenant.CreatedAt,
            UpdatedAt = tenant.UpdatedAt
        };
    }
}