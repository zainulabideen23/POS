using POS.Identity.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);



    }
}
