using POS.Operations.Application.DTOs;
using POS.Organization.Application.Interfaces;
using POS.Organization.Domain.Entities;
using POS.Shared.Application.Interfaces;


namespace POS.Operations.Application.Services
{
   

    public class RegisterSessionService : IRegisterSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUser _currentUser;

        public RegisterSessionService(IUnitOfWork unitOfWork, ICurrentUser currentUser)
        {
            _unitOfWork = unitOfWork ;
            _currentUser = currentUser ;
        }

        public async Task<IEnumerable<RegisterDropdownResponse>> GetAvailableRegistersAsync(CancellationToken cancellationToken = default)
        {
            // Note: Assuming you have a generic GetAllAsync or a specific method in your POSRegister repository
            var registers = await _unitOfWork.POSRegisters.GetAllAsync(cancellationToken);

            return registers.Select(r => new RegisterDropdownResponse
            {
                Id = r.Id,
                Name = r.Name,
                Code = r.Code
            });
        }

        public async Task<RegisterSessionResponse> OpenSessionAsync(OpenSessionRequest request, CancellationToken cancellationToken = default)
        {
            // 1. Verify the register exists
            var register = await _unitOfWork.POSRegisters.GetByIdAsync(request.RegisterId, cancellationToken);
            if (register == null)
            {
                throw new KeyNotFoundException("The selected register does not exist.");
            }

            // 2. Prevent opening multiple sessions on the same register
            var isAlreadyOpen = await _unitOfWork.RegisterSession.IsSessionOpenAsync(request.RegisterId, cancellationToken);
            if (isAlreadyOpen)
            {
                throw new InvalidOperationException($"Register '{register.Name}' already has an open session.");
            }

            // 3. Create the session
            var session = new RegisterSession
            {
                Id = Guid.NewGuid(),
                TenantId = _currentUser.TenantId,
                RegisterId = request.RegisterId,
                OpenedByUserId = _currentUser.UserId,
                OpenedAt = DateTime.UtcNow,
                OpeningCash = request.OpeningCash,
                Status = "Open"
            };

            await _unitOfWork.RegisterSession.AddAsync(session, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new RegisterSessionResponse
            {
                Id = session.Id,
                RegisterId = session.RegisterId,
                OpeningCash = session.OpeningCash,
                OpenedAt = session.OpenedAt,
                Status = session.Status
            };
        }
    }
}