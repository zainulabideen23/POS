using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.DTOs.Roles
{
    public class RoleResponse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
