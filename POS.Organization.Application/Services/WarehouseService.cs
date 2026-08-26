using POS.Organization.Application.DTOs;
using POS.Organization.Application.Interfaces;
using POS.Organization.Domain.Entities;
using POS.Shared.Application.Interfaces;


namespace POS.Organization.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public WarehouseService(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<WarehouseResponse> CreateWarehouseAsync(CreateWarehouseRequest request, CancellationToken cancellationToken = default)
        {
            var tenantId = _currentUser.TenantId;

            // Uniqueness Checks
            if (!await _unitOfWork.Warehouse.IsCodeUniqueAsync(request.Code, tenantId, null, cancellationToken))
                throw new InvalidOperationException($"A warehouse with the code '{request.Code}' already exists.");

            if (!await _unitOfWork.Warehouse.IsNameUniqueAsync(request.Name, tenantId, null, cancellationToken))
                throw new InvalidOperationException($"A warehouse with the name '{request.Name}' already exists.");

            var warehouse = new Warehouse
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                BranchId = _currentUser.BranchId,
                Name = request.Name,
                Code = request.Code,
                Address = request.Address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Warehouse.AddAsync(warehouse, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToResponse(warehouse);
        }

        public async Task UpdateWarehouseAsync(Guid id, UpdateWarehouseRequest request, CancellationToken cancellationToken = default)
        {
            var warehouse = await _unitOfWork.Warehouse.GetByIdAsync(id, cancellationToken);
            if (warehouse == null)
                throw new KeyNotFoundException($"Warehouse with ID {id} was not found.");

            var tenantId = _currentUser.TenantId;

            // Uniqueness Checks (Notice we pass the 'id' to exclude the current record from the check)
            if (!await _unitOfWork.Warehouse.IsCodeUniqueAsync(request.Code, tenantId, id, cancellationToken))
                throw new InvalidOperationException($"A warehouse with the code '{request.Code}' already exists.");

            if (!await _unitOfWork.Warehouse.IsNameUniqueAsync(request.Name, tenantId, id, cancellationToken))
                throw new InvalidOperationException($"A warehouse with the name '{request.Name}' already exists.");

            warehouse.Name = request.Name;
            warehouse.Code = request.Code;
            warehouse.Address = request.Address;
            warehouse.IsActive = request.IsActive;

            _unitOfWork.Warehouse.Update(warehouse);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        public async Task<WarehouseResponse> GetWarehouseByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var warehouse = await _unitOfWork.Warehouse.GetByIdAsync(id, cancellationToken);

            if (warehouse == null)
            {
                return null;
            }

            return MapToResponse(warehouse);
        }

        public async Task<IEnumerable<WarehouseResponse>> GetAllWarehousesAsync(CancellationToken cancellationToken = default)
        {
            var warehouses = await _unitOfWork.Warehouse.GetAllAsync(cancellationToken);
            return warehouses.Select(MapToResponse);
        }

       

        public async Task DeleteWarehouseAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var warehouse = await _unitOfWork.Warehouse.GetByIdAsync(id, cancellationToken);

            if (warehouse == null)
            {
                throw new KeyNotFoundException($"Warehouse with ID {id} was not found.");
            }

            _unitOfWork.Warehouse.Delete(warehouse);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        // Updated manual mapper to use the class instantiation
        private static WarehouseResponse MapToResponse(Warehouse warehouse)
        {
            return new WarehouseResponse
            {
                Id = warehouse.Id,
                Name = warehouse.Name,
                Code = warehouse.Code,
                Address = warehouse.Address,
                IsActive = warehouse.IsActive
            };
        }
    }
}