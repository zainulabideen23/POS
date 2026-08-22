namespace POS.Shared.Application.Interfaces;

public interface ICurrentUser
{
    Guid UserId { get; }
    Guid TenantId { get; }
    Guid BranchId { get; }
}