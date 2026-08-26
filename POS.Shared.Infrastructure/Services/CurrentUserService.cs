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
                .FindFirstValue("tenant_id");

            return Guid.TryParse(value, out var id)
                ? id
                : Guid.Empty;
        }
    }

    public Guid BranchId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue("branch_id");

            return Guid.TryParse(value, out var id)
                ? id
                : Guid.Empty;
        }
    }
}