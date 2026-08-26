using System;

namespace POS.Operations.Application.DTOs
{
    // Used to populate the dropdown on the frontend
    public class RegisterDropdownResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    // Sent by the frontend when the cashier clicks "Open Shift"
    public class OpenSessionRequest
    {
        public Guid RegisterId { get; set; }
        public decimal OpeningCash { get; set; }
    }

    // Returned to the frontend after successfully opening
    public class RegisterSessionResponse
    {
        public Guid Id { get; set; }
        public Guid RegisterId { get; set; }
        public decimal OpeningCash { get; set; }
        public DateTime OpenedAt { get; set; }
        public string Status { get; set; }
    }
}