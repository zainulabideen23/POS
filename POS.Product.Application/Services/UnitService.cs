using POS.Product.Application.Interfaces.UnitInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Product.Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantContext _tenantContext;

        public UnitService(
            IUnitRepository unitRepository,
            IUnitOfWork unitOfWork,
            ITenantContext tenantContext)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
            _tenantContext = tenantContext;
        }

        public async Task<IEnumerable<UnitResponseDto>> GetAllAsync()
        {
            var units = await _unitRepository.GetAllAsync();

            return units
                .Where(IsAccessibleToCurrentTenant)
                .Where(x => x.IsActive)
                .Select(Map);
        }

        public async Task<UnitResponseDto?> GetByIdAsync(Guid id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
                return null;

            if (!IsAccessibleToCurrentTenant(unit))
                return null;

            return Map(unit);
        }

        public async Task<IEnumerable<UnitResponseDto>>
            GetByCategoryAsync(UnitCategory category)
        {
            if (!Enum.IsDefined(category))
                throw new ArgumentException(
                    "Invalid unit category.");

            var units =
                await _unitRepository.GetByCategoryAsync(category);

            return units
                .Where(IsAccessibleToCurrentTenant)
                .Where(x => x.IsActive)
                .Select(Map);
        }

        public async Task<IEnumerable<UnitResponseDto>>
            SearchAsync(string searchTerm)
        {
            var units =
                await _unitRepository.SearchAsync(searchTerm);

            return units
                .Where(IsAccessibleToCurrentTenant)
                .Where(x => x.IsActive)
                .Select(Map);
        }

        public async Task<UnitResponseDto> CreateAsync(
            CreateUnitDto dto)
        {
            ValidateCreateDto(dto);

            var exists = await _unitRepository.ExistsAsync(
                dto.Category,
                dto.Name);

            if (exists)
            {
                throw new InvalidOperationException(
                    "A unit with this name already exists.");
            }

            var unit = Unit.CreateCustom(
                _tenantContext.TenantId,
                dto.Category,
                dto.Name,
                dto.ShortName,
                dto.ConversionFactor);

            await _unitRepository.AddAsync(unit);

            await _unitOfWork.SaveChangesAsync();

            return Map(unit);
        }

        public async Task<UnitResponseDto?> UpdateAsync(
            Guid id,
            UpdateUnitDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException(
                    "Unit name is required.");

            if (string.IsNullOrWhiteSpace(dto.ShortName))
                throw new ArgumentException(
                    "Short name is required.");

            if (dto.ConversionFactor <= 0)
                throw new ArgumentException(
                    "Conversion factor must be greater than zero.");

            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
                return null;

            if (!IsAccessibleToCurrentTenant(unit))
                return null;

            if (unit.IsSystemDefined)
            {
                throw new InvalidOperationException(
                    "System-defined units cannot be modified.");
            }

            var duplicate = await _unitRepository.ExistsAsync(
                unit.Category,
                dto.Name,
                id);

            if (duplicate)
            {
                throw new InvalidOperationException(
                    "A unit with this name already exists.");
            }

            unit.UpdateCustom(
                dto.Name,
                dto.ShortName,
                dto.ConversionFactor);

            _unitRepository.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return Map(unit);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
                return false;

            if (!IsAccessibleToCurrentTenant(unit))
                return false;

            if (unit.IsSystemDefined)
            {
                throw new InvalidOperationException(
                    "System-defined units cannot be deleted.");
            }

            // Soft delete.
            unit.Deactivate();

            _unitRepository.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateAsync(Guid id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
                return false;

            if (!IsAccessibleToCurrentTenant(unit))
                return false;

            unit.Activate();

            _unitRepository.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateAsync(Guid id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);

            if (unit == null)
                return false;

            if (!IsAccessibleToCurrentTenant(unit))
                return false;

            if (unit.IsSystemDefined)
            {
                throw new InvalidOperationException(
                    "System-defined units cannot be deactivated.");
            }

            unit.Deactivate();

            _unitRepository.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<ConvertUnitResponseDto> ConvertAsync(
            ConvertUnitDto dto)
        {
            if (dto.Quantity < 0)
            {
                throw new ArgumentException(
                    "Quantity cannot be negative.");
            }

            var fromUnit =
                await _unitRepository.GetByIdAsync(
                    dto.FromUnitId);

            var toUnit =
                await _unitRepository.GetByIdAsync(
                    dto.ToUnitId);

            if (fromUnit == null)
            {
                throw new KeyNotFoundException(
                    "Source unit not found.");
            }

            if (toUnit == null)
            {
                throw new KeyNotFoundException(
                    "Target unit not found.");
            }

            if (!IsAccessibleToCurrentTenant(fromUnit) ||
                !IsAccessibleToCurrentTenant(toUnit))
            {
                throw new KeyNotFoundException(
                    "Unit not found.");
            }

            if (!fromUnit.IsActive ||
                !toUnit.IsActive)
            {
                throw new InvalidOperationException(
                    "Both units must be active.");
            }

            if (fromUnit.Category != toUnit.Category)
            {
                throw new InvalidOperationException(
                    "Units from different categories cannot be converted.");
            }

            var result =
                dto.Quantity *
                fromUnit.ConversionFactor /
                toUnit.ConversionFactor;

            return new ConvertUnitResponseDto
            {
                FromUnit = fromUnit.Name,
                InputQuantity = dto.Quantity,
                ToUnit = toUnit.Name,
                ResultQuantity = result
            };
        }

        private bool IsAccessibleToCurrentTenant(Unit unit)
        {
            return unit.IsSystemDefined ||
                   unit.TenantId == _tenantContext.TenantId;
        }

        private static void ValidateCreateDto(
            CreateUnitDto dto)
        {
            if (!Enum.IsDefined(dto.Category))
            {
                throw new ArgumentException(
                    "Invalid unit category.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException(
                    "Unit name is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.ShortName))
            {
                throw new ArgumentException(
                    "Short name is required.");
            }

            if (dto.ConversionFactor <= 0)
            {
                throw new ArgumentException(
                    "Conversion factor must be greater than zero.");
            }
        }

        private static UnitResponseDto Map(Unit unit)
        {
            return new UnitResponseDto
            {
                Id = unit.Id,
                Category = unit.Category,
                CategoryName = unit.Category.ToString(),
                BaseUnitName =
                    UnitRules.GetBaseUnitName(unit.Category),
                Name = unit.Name,
                ShortName = unit.ShortName,
                ConversionFactor = unit.ConversionFactor,
                IsSystemDefined = unit.IsSystemDefined,
                IsActive = unit.IsActive
            };
        }

    }
}
