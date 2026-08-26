using POS.Organization.Application.DTOs;
using POS.Organization.Application.Interfaces;
using POS.Organization.Domain.Entities;
using POS.Shared.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.Organization.Application.Services
{
    public class POSRegisterService : IPOSRegisterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public POSRegisterService(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork ;
            _currentUser = currentUser ;
        }

        public async Task<POSRegisterResponse> CreateRegisterAsync(CreatePOSRegisterRequest request, CancellationToken cancellationToken = default)
        {
            var tenantId = _currentUser.TenantId;

            // Uniqueness Checks
            if (!await _unitOfWork.POSRegisters.IsCodeUniqueAsync(request.Code, tenantId, null, cancellationToken))
                throw new InvalidOperationException($"A register with the code '{request.Code}' already exists.");

            if (!await _unitOfWork.POSRegisters.IsNameUniqueAsync(request.Name, tenantId, null, cancellationToken))
                throw new InvalidOperationException($"A register with the name '{request.Name}' already exists.");

            var register = new POSRegister
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                BranchId = _currentUser.BranchId, // Links the terminal to a specific branch
                Name = request.Name,
                Code = request.Code,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.POSRegisters.AddAsync(register, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return MapToResponse(register);
        }

        public async Task<POSRegisterResponse> GetRegisterByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var register = await _unitOfWork.POSRegisters.GetByIdAsync(id, cancellationToken);
            return register == null ? null : MapToResponse(register);
        }

        public async Task<IEnumerable<POSRegisterResponse>> GetAllRegistersAsync(CancellationToken cancellationToken = default)
        {
            var registers = await _unitOfWork.POSRegisters.GetAllAsync(cancellationToken);
            return registers.Select(MapToResponse);
        }

        public async Task UpdateRegisterAsync(Guid id, UpdatePOSRegisterRequest request, CancellationToken cancellationToken = default)
        {
            var register = await _unitOfWork.POSRegisters.GetByIdAsync(id, cancellationToken);
            if (register == null)
                throw new KeyNotFoundException($"Register with ID {id} was not found.");

            var tenantId = _currentUser.TenantId;

            if (!await _unitOfWork.POSRegisters.IsCodeUniqueAsync(request.Code, tenantId, id, cancellationToken))
                throw new InvalidOperationException($"A register with the code '{request.Code}' already exists.");

            register.Name = request.Name;
            register.Code = request.Code;
            register.IsActive = request.IsActive;

            _unitOfWork.POSRegisters.Update(register);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRegisterAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var register = await _unitOfWork.POSRegisters.GetByIdAsync(id, cancellationToken);
            if (register == null)
                throw new KeyNotFoundException($"Register with ID {id} was not found.");

            _unitOfWork.POSRegisters.Delete(register);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static POSRegisterResponse MapToResponse(POSRegister register)
        {
            return new POSRegisterResponse
            {
                Id = register.Id,
                Name = register.Name,
                Code = register.Code,
                IsActive = register.IsActive
            };
        }
    }
}