// POS.Identity.Application/Interfaces/ITokenService.cs
using POS.Identity.Domain.Entities;
using System.Security.Claims;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, Guid tenantId, Guid branchId);
}