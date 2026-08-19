using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; } 
        public string Password { get; set; }
        public string TenantName{ get; set; }
        public string BranchName { get; set; }
        public string RoleName { get; set; }
    }
}
