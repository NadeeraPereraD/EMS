using System;
using System.Collections.Generic;

namespace EMS.API.Models.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string name { get; set; } = null!;

    public string email { get; set; } = null!;

    public string jobPosition { get; set; } = null!;

    public bool isActive { get; set; }
}
