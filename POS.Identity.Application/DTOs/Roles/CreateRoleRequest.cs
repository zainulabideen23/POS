using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.DTOs.Roles
{
    public class CreateRoleRequest
    {
        public required string RoleName { get; set; }
    }
}
