using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Domain.Entities
{
    public class RegisterSession
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        //POS register id
        public Guid RegisterId { get; set; }

        public Guid OpenedByUserId { get; set; }
        public Guid? ClosedByUserId { get; set; } // Nullable, as the session might still be open

        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        public decimal OpeningCash { get; set; }
        public decimal? ExpectedClosingCash { get; set; }
        public decimal? ActualClosingCash { get; set; }
        public decimal? CashDifference { get; set; }

        public string Status { get; set; } // Alternatively, this could be an enum (e.g., SessionStatus.Open, SessionStatus.Closed)

        // Navigation Property
        public POSRegister Register { get; set; }
    }
}
