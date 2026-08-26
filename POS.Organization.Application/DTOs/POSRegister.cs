using System;

namespace POS.Organization.Application.DTOs
{
    public class CreatePOSRegisterRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class UpdatePOSRegisterRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }

    public class POSRegisterResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }
}