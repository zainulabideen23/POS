using POS.Product.Application.Dtos.Unit;
using POS.Product.Application.Interfaces;
using POS.Product.Application.Interfaces.UnitInterfaces;
using POS.Product.Application.Units;
using POS.Product.Domain.Entities;
using POS.Product.Domain.Enum;
using POS.Shared.Application.Interfaces;

namespace POS.Product.Application.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public UnitService(
            IUnitOfWork unitOfWork,
            ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<IEnumerable<UnitResponseDto>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var units = await _unitOfWork.Units.GetAllAsync();

            return units
                .Where(IsAccessibleToCurrentTenant)
                .Where(x => x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<UnitResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var unit = await _unitOfWork.Units.GetByIdAsync(id);

            if (unit == null)
                return null;

            if (!IsAccessibleToCurrentTenant(unit))
                return null;

            return Map(unit);
        }

        public async Task<IEnumerable<UnitResponseDto>> GetByCategoryAsync(UnitCategory category,
            CancellationToken cancellationToken = default)
        {
            if (!Enum.IsDefined(category))
                throw new ArgumentException("Invalid unit category.");

            var units = await _unitOfWork.Units.GetByCategoryAsync(category);

            return units
                .Where(IsAccessibleToCurrentTenant)
                .Where(x => x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<IEnumerable<UnitResponseDto>> SearchAsync(
            string searchTerm,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return Enumerable.Empty<UnitResponseDto>();

            var units = await _unitOfWork.Units.SearchAsync(searchTerm);

            return units
                .Where(IsAccessibleToCurrentTenant)
                .Where(x => x.IsActive)
                .Select(Map)
                .ToList();
        }

        public async Task<UnitResponseDto> CreateAsync(
            CreateUnitDto dto,
            CancellationToken cancellationToken = default)
        {
            ValidateCreateDto(dto);

            var exists = await _unitOfWork.Units.ExistsAsync(
                dto.Category,
                dto.Name);

            if (exists)
                throw new InvalidOperationException(
                    "A unit with this name already exists.");

            var unit = Unit.CreateCustom(
                _currentUser.UserId,
                _currentUser.TenantId,
                dto.Category,
                dto.Name,
                dto.ShortName,
                dto.ConversionFactor);

            await _unitOfWork.Units.AddAsync(unit);

            await _unitOfWork.SaveChangesAsync();

            return Map(unit);
        }

        public async Task<UnitResponseDto?> UpdateAsync(
            Guid id,
            UpdateUnitDto dto,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Unit name is required.");

            if (string.IsNullOrWhiteSpace(dto.ShortName))
                throw new ArgumentException("Unit short name is required.");

            if (dto.ConversionFactor <= 0)
                throw new ArgumentException(
                    "Conversion factor must be greater than zero.");

            var unit = await _unitOfWork.Units.GetByIdAsync(id);

            if (unit == null)
                return null;

            if (!IsAccessibleToCurrentTenant(unit))
                return null;

            if (unit.IsSystemDefined)
            {
                throw new InvalidOperationException(
                    "System-defined units cannot be modified.");
            }

            var duplicate = await _unitOfWork.Units.ExistsAsync(
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

            _unitOfWork.Units.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return Map(unit);
        }

        public async Task<bool> DeleteAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var unit = await _unitOfWork.Units.GetByIdAsync(id);

            if (unit == null)
                return false;

            if (!IsAccessibleToCurrentTenant(unit))
                return false;

            if (unit.IsSystemDefined)
            {
                throw new InvalidOperationException(
                    "System-defined units cannot be deleted.");
            }

            // Soft delete
            unit.Deactivate();

            _unitOfWork.Units.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ActivateAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var unit = await _unitOfWork.Units.GetByIdAsync(id);

            if (unit == null)
                return false;

            if (!IsAccessibleToCurrentTenant(unit))
                return false;

            unit.Activate();

            _unitOfWork.Units.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeactivateAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var unit = await _unitOfWork.Units.GetByIdAsync(id);

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

            _unitOfWork.Units.Update(unit);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<ConvertUnitResponseDto> ConvertAsync(
            ConvertUnitDto dto,
            CancellationToken cancellationToken = default)
        {
            if (dto.Quantity < 0)
                throw new ArgumentException(
                    "Quantity cannot be negative.");

            var fromUnit = await _unitOfWork.Units.GetByIdAsync(
                dto.FromUnitId);

            var toUnit = await _unitOfWork.Units.GetByIdAsync(
                dto.ToUnitId);

            if (fromUnit == null)
                throw new KeyNotFoundException(
                    "Source unit not found.");

            if (toUnit == null)
                throw new KeyNotFoundException(
                    "Target unit not found.");

            if (!IsAccessibleToCurrentTenant(fromUnit) ||
                !IsAccessibleToCurrentTenant(toUnit))
            {
                throw new KeyNotFoundException(
                    "Unit not found.");
            }

            if (!fromUnit.IsActive || !toUnit.IsActive)
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
            return unit.IsSystemDefined || unit.TenantId == _currentUser.TenantId;
        }

        private static void ValidateCreateDto(CreateUnitDto dto)
        {
            if (!Enum.IsDefined(dto.Category))
                throw new ArgumentException(
                    "Invalid unit category.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException(
                    "Unit name is required.");

            if (string.IsNullOrWhiteSpace(dto.ShortName))
                throw new ArgumentException(
                    "Unit short name is required.");

            if (dto.ConversionFactor <= 0)
                throw new ArgumentException(
                    "Conversion factor must be greater than zero.");
        }

        private static UnitResponseDto Map(Unit unit)
        {
            return new UnitResponseDto
            {
                Id = unit.Id,
                Category = unit.Category,
                CategoryName = unit.Category.ToString(),
                BaseUnitName = UnitRules.GetBaseUnitName(unit.Category),
                Name = unit.Name,
                ShortName = unit.ShortName,
                ConversionFactor = unit.ConversionFactor,
                IsSystemDefined = unit.IsSystemDefined,
                IsActive = unit.IsActive
            };
        }
    }
}
