using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.DTOs
{
    public class CreateWarehouseRequest
    {
    public string Name { get; set; }
    public string Code { get; set; }
    public string Address { get; set; } = string.Empty;
    }
}
