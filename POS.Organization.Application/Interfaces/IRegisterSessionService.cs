using POS.Operations.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.Interfaces
{
    public interface IRegisterSessionService
    {
        Task<IEnumerable<RegisterDropdownResponse>> GetAvailableRegistersAsync(CancellationToken cancellationToken = default);
        Task<RegisterSessionResponse> OpenSessionAsync(OpenSessionRequest request, CancellationToken cancellationToken = default);
    }
}
