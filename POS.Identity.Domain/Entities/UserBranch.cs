using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Identity.Domain.Entities
{
    public class UserBranch
    {
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;

        public int BranchId { get; set; }
         public Branch Branch { get; set; }
    }
}
