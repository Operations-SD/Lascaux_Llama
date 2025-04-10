using System;
using System.Collections.Generic;

namespace IntelChat.Models;

public partial class LpMajor
{
    public int LpMajorId { get; set; }

    public string LpMajorLabel { get; set; } = null!;

    public string LpMajorDesc { get; set; } = null!;

    public string LpMajorType { get; set; } = null!;

    public string LpMajorStatus { get; set; } = null!;

    public short LpMajorSeq { get; set; }

    public string LpMajorRgb { get; set; } = null!;

    public DateTime? LpMajorDtime { get; set; }

    public int? PersonId { get; set; }
}
