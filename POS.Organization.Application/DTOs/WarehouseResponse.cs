using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Organization.Application.DTOs
{
    public class WarehouseResponse
    {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code {  get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }
    }
}
