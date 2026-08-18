
using POS.Identity.Application.DTOs;
using POS.Identity.Application.Interfaces;

namespace POS.Application.Services;

public class BranchService
{
    private readonly IUnitOfWork _unitOfWork;

    public BranchService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<BranchResponseDto>>
        GetAllAsync()
    {
        var branches =
            await _unitOfWork.Branches.GetAllAsync();

        return branches.Select(MapToResponse);
    }

    public async Task<BranchResponseDto?>
        GetByIdAsync(int id)
    {
        var branch =
            await _unitOfWork.Branches.GetByIdAsync(id);

        if (branch == null)
            return null;

        return MapToResponse(branch);
    }

    public async Task<IEnumerable<BranchResponseDto>>
        GetByTenantIdAsync(int tenantId)
    {
        var branches =
            await _unitOfWork.Branches
                .GetByTenantIdAsync(tenantId);

        return branches.Select(MapToResponse);
    }

    public async Task<BranchResponseDto?>
        CreateAsync(CreateBranchDto dto)
    {
        // Verify Tenant exists
        var tenant =
            await _unitOfWork.Tenants
                .GetByIdAsync(dto.TenantId);

        if (tenant == null)
            return null;

        var branch = new Branch
        {
            TenantId = dto.TenantId,
            Name = dto.Name,
            Code = dto.Code,
            Phone = dto.Phone,
            Email = dto.Email,
            Address = dto.Address
        };

        await _unitOfWork.Branches.AddAsync(branch);

        await _unitOfWork.SaveChangesAsync();

        return MapToResponse(branch);
    }

    public async Task<bool>
        UpdateAsync(int id, UpdateBranchDto dto)
    {
        var branch =
            await _unitOfWork.Branches.GetByIdAsync(id);

        if (branch == null)
            return false;

        branch.Name = dto.Name;
        branch.Code = dto.Code;
        branch.Phone = dto.Phone;
        branch.Email = dto.Email;
        branch.Address = dto.Address;
        branch.IsActive = dto.IsActive;
        branch.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Branches.Update(branch);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool>
        DeleteAsync(int id)
    {
        var branch =
            await _unitOfWork.Branches.GetByIdAsync(id);

        if (branch == null)
            return false;

        _unitOfWork.Branches.Delete(branch);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private static BranchResponseDto MapToResponse(
        Branch branch)
    {
        return new BranchResponseDto
        {
            Id = branch.Id,
            TenantId = branch.TenantId,
            Name = branch.Name,
            Code = branch.Code,
            Phone = branch.Phone,
            Email = branch.Email,
            Address = branch.Address,
            IsActive = branch.IsActive,
            CreatedAt = branch.CreatedAt,
            UpdatedAt = branch.UpdatedAt
        };
    }
}