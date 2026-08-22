using Microsoft.AspNetCore.Http;
using POS.Shared.Application.Interfaces;
using System.Security.Claims;

namespace POS.Shared.Infrastructure.Services;

public class CurrentUserService : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(value, out var id)
                ? id
                : Guid.Empty;
        }
    }

    public Guid TenantId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirst("tenant_id")
                ?.Value;

            return Guid.TryParse(value, out var id)
                ? id
                : Guid.Empty;
        }
    }

    public int BranchId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirst("branch_id")
                ?.Value;

            return int.TryParse(value, out var id) ? id : 0;
        }
    }

    Guid ICurrentUser.TenantId => throw new NotImplementedException();

    Guid ICurrentUser.BranchId => throw new NotImplementedException();
}