using POS.Product.Application.Dtos.Unit;
using POS.Product.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.UnitInterfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitResponseDto>> GetAllAsync(CancellationToken cancellationToken=default);
        Task<UnitResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<UnitResponseDto>> GetByCategoryAsync(UnitCategory category, CancellationToken cancellationToken = default);
        Task<IEnumerable<UnitResponseDto>> SearchAsync(string searchTerm, CancellationToken cancellationToken = default);
        Task<UnitResponseDto> CreateAsync(CreateUnitDto dto, CancellationToken cancellationToken = default);
        Task<UnitResponseDto?> UpdateAsync(Guid id, UpdateUnitDto dto, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ConvertUnitResponseDto> ConvertAsync(ConvertUnitDto dto, CancellationToken cancellationToken = default);
    }
}
