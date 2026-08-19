using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.DTOs.Roles
{
    public class AssignRoleRequest
    {
        public required string UserId { get; set; }
        public required string RoleName { get; set; }
    }
}
