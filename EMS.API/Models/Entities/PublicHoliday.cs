using System;
using System.Collections.Generic;

namespace EMS.API.Models.Entities;

public partial class PublicHoliday
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public string? Description { get; set; }
}
