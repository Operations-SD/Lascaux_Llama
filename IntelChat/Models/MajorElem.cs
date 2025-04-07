using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class MajorElem
{
    public int? LpMajorId { get; set; }

    public string? LpMajorLabel { get; set; }

    public string LpElemLabel { get; set; } = null!;
}
