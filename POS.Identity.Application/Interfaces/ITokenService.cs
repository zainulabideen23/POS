// POS.Identity.Application/Interfaces/ITokenService.cs
using POS.Identity.Domain.Entities;
using System.Security.Claims;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user, int tenantId, IList<string> permissions);
}