using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class AuditCountTimeSlot
{
    public int Pid { get; set; }

    public string Person { get; set; } = null!;

    public short Week { get; set; }

    public string Count { get; set; } = null!;

    public string Primitive { get; set; } = null!;

    public byte DOfW { get; set; }

    public byte Minutes { get; set; }

    public double MultBy { get; set; }
}
