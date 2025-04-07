using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class ViewTimeslot
{
    public int? LpMajorId { get; set; }

    public string? LpMajorLabel { get; set; }

    public int LpMinorId { get; set; }

    public string LpMinorLabel { get; set; } = null!;

    public string? Expr1 { get; set; }

    public string? LpElemLabel { get; set; }
}
