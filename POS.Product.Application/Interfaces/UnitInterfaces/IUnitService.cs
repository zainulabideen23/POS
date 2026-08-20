using POS.Product.Application.Dtos.Unit;
using POS.Product.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Interfaces.UnitInterfaces
{
    public interface IUnitService
    {
        Task<IEnumerable<UnitResponseDto>> GetAllAsync();
        Task<UnitResponseDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<UnitResponseDto>> GetByCategoryAsync(UnitCategory category);
        Task<IEnumerable<UnitResponseDto>> SearchAsync(string searchTerm);
        Task<UnitResponseDto> CreateAsync(CreateUnitDto dto);
        Task<UnitResponseDto?> UpdateAsync(Guid id, UpdateUnitDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ActivateAsync(Guid id);
        Task<bool> DeactivateAsync(Guid id);
        Task<ConvertUnitResponseDto> ConvertAsync(ConvertUnitDto dto);
    }
}
