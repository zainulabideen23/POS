using POS.Identity.Application.DTOs;
using POS.Identity.Application.Interfaces;
using POS.Shared.Application.Interfaces;

namespace POS.Application.Services;

public class BranchService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public BranchService(
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser)
    {
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<BranchResponseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var tenantId = _currentUser.TenantId;

        var branches = await _unitOfWork.Branches
            .GetAllByTenantIdAsync(tenantId,cancellationToken);

        return branches.Select(MapToResponse);
    }

    public async Task<BranchResponseDto?> GetByIdAsync(int id)
    {
        var tenantId = _currentUser.TenantId;

        var branch = await _unitOfWork.Branches
            .GetByIdAndTenantIdAsync(id, tenantId);

        if (branch == null)
            return null;

        return MapToResponse(branch);
    }

    public async Task<BranchResponseDto?> CreateAsync(CreateBranchDto dto)
    {
        var tenantId = _currentUser.TenantId;

        var tenant = await _unitOfWork.Tenants
            .GetByIdAsync(tenantId);

        if (tenant == null)
            return null;

        var branch = new Branch
        {
            TenantId = tenantId,
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

    public async Task<bool> UpdateAsync(
        int id,
        UpdateBranchDto dto)
    {
        var tenantId = _currentUser.TenantId;

        var branch = await _unitOfWork.Branches
            .GetByIdAndTenantIdAsync(id, tenantId);

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

    public async Task<bool> DeleteAsync(int id)
    {
        var tenantId = _currentUser.TenantId;

        var branch = await _unitOfWork.Branches
            .GetByIdAndTenantIdAsync(id, tenantId);

        if (branch == null)
            return false;

        _unitOfWork.Branches.Delete(branch);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    private static BranchResponseDto MapToResponse(Branch branch)
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