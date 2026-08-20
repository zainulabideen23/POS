namespace POS.Shared.Application.Interfaces;

public interface ICurrentUser
{
    int UserId { get; }
    int TenantId { get; }
    int BranchId { get; }
}