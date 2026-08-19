using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Application.DTOs
{
    public class CreateBranchDto
    {
      
        public string Name { get; set; } = string.Empty;
        public string? Code { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
